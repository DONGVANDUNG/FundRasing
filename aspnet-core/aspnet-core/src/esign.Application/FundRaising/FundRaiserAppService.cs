using Abp.Domain.Repositories;
using Abp.UI;
using esign.Entity;
using esign.FundRaising.FundRaiserService;
using esign.FundRaising.FundRaiserService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using esign.Authorization.Users;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Twilio.Rest.Api.V2010.Account;
using Microsoft.AspNetCore.Http;
using System.IO;
using esign.FundRaising.Admin.Dto;
using esign.Enitity;
using Microsoft.AspNetCore.Mvc;
using esign.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Dapper;

namespace esign.FundRaising
{
    public class FundRaiserAppService : esignAppServiceBase, IFundRaiser
    {
        private readonly IRepository<Funds, long> _mstSleFundRepo;
        private readonly IRepository<FundTransactions, int> _mstSleFundTransactionRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<FundDetailContent, long> _mstSleDetailConentRepo;
        private readonly IRepository<UserWarning, int> _mstSleUserWarningRepo;
        private readonly IRepository<UserAccount, int> _mstSleUserAccountRepo;
        private readonly IRepository<FundRaiser, long> _mstSleFundRaiserRepo;
        private readonly IRepository<FundImage, long> _mstSleFundImageRepo;
        private readonly IConfigurationRoot _appConfiguration;
        public FundRaiserAppService(IRepository<Funds, long> mstSleFundRepo,
            IRepository<FundTransactions, int> mstSleFundTransactionRepo,
            IRepository<User, long> mstSleUserRepo,
            IWebHostEnvironment hostingEnvironment,
            IWebHostEnvironment env,
            IRepository<FundDetailContent, long> mstSleDetailConentRepo,
            IRepository<UserWarning, int> mstSleUserWarningRepo,
            IRepository<UserAccount, int> mstSleUserAccountRepo,
            IRepository<FundRaiser, long> mstSleFundRaiserRepo,
            IRepository<FundImage, long> mstSleFundImageRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _mstSleDetailConentRepo = mstSleDetailConentRepo;
            _mstSleUserWarningRepo = mstSleUserWarningRepo;
            _mstSleUserAccountRepo = mstSleUserAccountRepo;
            _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundImageRepo = mstSleFundImageRepo;
            _appConfiguration = env.GetAppConfiguration();
            _appConfiguration = hostingEnvironment.GetAppConfiguration();
        }
        public async Task CloseFundRaising(int fundId)
        {
            try
            {
                var fund = await _mstSleFundRepo.FirstOrDefaultAsync(s => s.Id == fundId);
                if (fund != null)
                {
                    fund.Status = 3;
                }
                await _mstSleFundRepo.UpdateAsync(fund);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Có lỗi xảy ra trong quá trình thêm mới");
            }
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task CreateFundRaising([FromForm] CreateOrEditFundRaisingInputDto input)
        {
            try
            {
                Funds fundInsert = new Funds();
                fundInsert.FundRaiserId = AbpSession.UserId;
                fundInsert.FundRaisingDay = input.FundStartDate;
                fundInsert.FundEndDate = input.FundEndDate;
                fundInsert.FundName = input.FundName;
                fundInsert.FundTitle = input.FundTitle;
                fundInsert.AmountOfMoney = input.AmountOfMoney;
                fundInsert.Status = 1;
                fundInsert.IsPayFee = input.IsPayFee;
                var fundId = await _mstSleFundRepo.InsertAndGetIdAsync(fundInsert);
                FundDetailContent fundDetail = new FundDetailContent();
                fundDetail.FundId = (int)fundId;
                fundDetail.Content = input.FundContent;
                fundDetail.ReasonCreatedFund = input.ReasonCreateFund;
                await _mstSleDetailConentRepo.InsertAsync(fundDetail);
                if (input.File.Count() > 0)
                {
                    foreach (var file in input.File)
                    {
                        if (file != null)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            FundImage fundImage = new FundImage();
                            fundImage.FundId = fundId;

                            fundImage.ImageUrl = Path.Combine("uploads", fileName);
                            await _mstSleFundImageRepo.InsertAsync(fundImage);
                        }
                    }
                }
                else
                {
                    throw new UserFriendlyException("Please select a file");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new UserFriendlyException("Error uploading file");
            }
        }

        public async Task UpdateFundRaising(CreateOrEditFundRaisingDto input)
        {

            try
            {
                var fund = await _mstSleFundRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
                if (fund != null)
                {
                    fund.FundRaiserId = AbpSession.UserId;
                    fund.FundRaisingDay = DateTime.Now;
                    fund.FundName = input.FundName;
                    fund.FundImageUrl = input.ImageUrl;
                    fund.FundTitle = input.TitleFund;
                    fund.AmountOfMoney = input.AmountOfMoney;
                    fund.FundEndDate = input.FundEndDate;
                    await _mstSleFundRepo.UpdateAsync(fund);
                    FundDetailContent detailContent = new FundDetailContent();
                    ObjectMapper.Map(input.ContentOfFund, detailContent);
                    detailContent.FundId = (int)fund.Id;
                    await _mstSleDetailConentRepo.UpdateAsync(detailContent);
                }
            }
            catch (Exception e) { }
        }

        public async Task ExtendTimeOfFundRaising(DateTime timeExtend, int fundId)
        {
            var fund = await _mstSleFundRepo.FirstOrDefaultAsync(e => e.Id == fundId);
            if (fund != null)
            {
                fund.FundEndDate = timeExtend;
                fund.Status = 2;
                await _mstSleFundRepo.UpdateAsync(fund);
            }
        }

        public async Task<List<TransactionOfFundForDto>> getListTransactionForFund(int fundId)
        {
            var listTransaction = (from transaction in _mstSleFundTransactionRepo.GetAll().Where(e => e.Id == fundId)
                                   join fund in _mstSleFundRepo.GetAll() on transaction.FundId equals fund.Id
                                   join user in _mstSleUserRepo.GetAll() on transaction.EmailReceiver equals user.Email
                                   select new TransactionOfFundForDto
                                   {
                                       Id = transaction.Id,
                                       Sender = user.Name,
                                       Amount = transaction.AmountOfMoney,
                                       Content = transaction.MessageToFund,
                                       FundName = fund.FundName
                                   }).ToListAsync();
            return await listTransaction;
        }

        public async Task<List<UserWarningForDto>> getListWarningOfUser()
        {
            var listWarning = (from userWarning in _mstSleUserWarningRepo.GetAll()
                               join user in _mstSleUserRepo.GetAll() on userWarning.UserId equals user.Id
                               select new UserWarningForDto
                               {
                                   Id = userWarning.Id,
                                   Content = userWarning.ContentWarning,
                                   LevelWarning = userWarning.LevelWarning == 1 ? "Cảnh cáo" : userWarning.LevelWarning == 2 ? "Khẩn cấp" : "Khóa",
                                   CreatedWarning = userWarning.CreationTime
                               }).ToListAsync();
            return await listWarning;
        }

        public async Task UpdateImageUrlForFund(string imageUrl, int fundId)
        {
            try
            {
                var fund = await _mstSleFundRepo.FirstOrDefaultAsync(e => e.Id == fundId);
                if (fund != null)
                {
                    fund.FundImageUrl = imageUrl;
                    await _mstSleFundRepo.UpdateAsync(fund);
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Có lỗi xảy ra trong quá trình cập nhật");
            }
        }
        //public async Task UpdateInformation(UpdateInformationFundRaiserDto input)
        //{


        //    var user = await _mstSleUserRepo.FirstOrDefaultAsync(e => e.Id == AbpSession.UserId);
        //    if (user != null)
        //    {
        //        ObjectMapper.Map(input, user);
        //        await _mstSleUserRepo.UpdateAsync(user);
        //        var userAccount = await _mstSleUserAccountRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
        //        userAccount.UserNameLogin = input.UserNameLogin;
        //        //userAccount.Password = input.Password;
        //        await _mstSleUserAccountRepo.UpdateAsync(userAccount);
        //    }
        //}
        public async Task RegisterFundRaiser(RegisterInforFundRaiserDto input)
        {
            try
            {
                FundRaiser fundRaiser = new FundRaiser();
                fundRaiser.Name = input.Name;
                fundRaiser.Email = input.Email;
                fundRaiser.Phone = input.Phone;
                fundRaiser.FundPackageId = 1;
                fundRaiser.UserId = AbpSession.UserId;
                fundRaiser.Position = input.Position;
                fundRaiser.BankNumber = input.BankNumber;
                fundRaiser.BankName = input.BankName;
                fundRaiser.Unit = "USD";
                fundRaiser.Surplus = 100;
                await _mstSleFundRaiserRepo.InsertAsync(fundRaiser);
                var user = _mstSleUserRepo.FirstOrDefault(e => e.Id == AbpSession.UserId);
                user.TypeUser = 3;
                await _mstSleUserRepo.UpdateAsync(user);
                using (SqlConnection connection = new SqlConnection(_appConfiguration.GetConnectionString("Default")))
                {

                    connection.Execute(@"
                            INSERT INTO dbo.AbpPermissions (CreationTime, CreatorUserId, Discriminator, IsGranted, Name ,RoleId,UserId) VALUES
                            (GetDate(),@p_userId, 'UserPermissionSetting',1,'Pages',3,@p_userId)
                        ", new
                    {
                        p_userId = AbpSession.UserId
                    });

                    connection.Execute(@"
                            INSERT INTO dbo.AbpPermissions (CreationTime, CreatorUserId, Discriminator, IsGranted, Name,RoleId ,UserId) VALUES
                            (GetDate(),@p_userId, 'UserPermissionSetting',1,'Pages.UserDonate',3,@p_userId)
                        ", new
                    {
                        p_userId = AbpSession.UserId
                    });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<RegisterInforFundRaiserDto> GetForEditFundRaiser()
        {
            try
            {
                var fundRaiser = await _mstSleFundRaiserRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
                if (fundRaiser?.Id != null)
                {
                    var fundRaiserEdit = new RegisterInforFundRaiserDto();
                    ObjectMapper.Map(fundRaiser, fundRaiserEdit);
                    return fundRaiserEdit;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task UpdateInforFundRaiser(RegisterInforFundRaiserDto input)
        {
            try
            {
                var fundRaiser = await _mstSleFundRaiserRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
                ObjectMapper.Map(input, fundRaiser);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
