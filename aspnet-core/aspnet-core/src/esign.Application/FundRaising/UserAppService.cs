﻿using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Dapper;
using esign.Authorization.Users;
using esign.Configuration;
using esign.Enitity;
using esign.Entity;
using esign.FundRaising.FundRaiserService;
using esign.FundRaising.FundRaiserService.Dto;
using esign.FundRaising.UserFundRaising.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using NUglify.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Twilio.Rest.Trunking.V1;

namespace esign.FundRaising
{
    public class UserAppService : esignAppServiceBase, IUserFundRaising
    {
        private readonly IRepository<Funds, long> _mstSleFundRepo;
        //private readonly IRepository<FundRaiser, long> _mstSleFundRaiserRepo;
        private readonly IRepository<FundDetailContent, long> _mstSleFundDetailContentRepo;
        private readonly IRepository<FundRaisingTopic, int> _mstSleFundTopictRepo;
        private readonly IRepository<FundPackage, int> _mstSleFundPackageRepo;
        private readonly IRepository<FundTransactions, int> _mstSleFundTransactionRepo;
        private readonly IRepository<UserAccount, int> _mstSleUserAccountRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<FundImage, long> _mstSleFundImageRepo;
        private readonly IRepository<BankAccount, long> _mstBankRepo;
        private readonly IConfigurationRoot _appConfiguration;


        public UserAppService(IRepository<Funds, long> mstSleFundRepo,
            //IRepository<FundRaiser, long>
            // mstSleFundRaiserRepo,
            IRepository<FundDetailContent, long> mstSleFundDetailContentRepo,
            IWebHostEnvironment hostingEnvironment, IWebHostEnvironment env,
            IRepository<FundRaisingTopic, int> mstSleFundTopictRepo,
            IRepository<FundPackage, int> mstSleFundPackageRepo,
            IRepository<FundTransactions, int> mstSleFundTransactionRepo,
            IRepository<UserAccount, int> mstSleUserAccountRepo,
            IRepository<User, long> mstSleUserRepo, IRepository<FundImage, long> mstSleFundImageRepo, IRepository<BankAccount, long> mstBankRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            /// _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundDetailContentRepo = mstSleFundDetailContentRepo;
            _mstSleFundTopictRepo = mstSleFundTopictRepo;
            _mstSleFundPackageRepo = mstSleFundPackageRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserAccountRepo = mstSleUserAccountRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _appConfiguration = env.GetAppConfiguration();
            _appConfiguration = hostingEnvironment.GetAppConfiguration();
            _mstSleFundImageRepo = mstSleFundImageRepo;
            _mstBankRepo = mstBankRepo;
        }
        public GetFundsDetailByIdForUser GetInforFundRaisingById(long Id)
        {
            var fund = (from funds in _mstSleFundRepo.GetAll().Where(e => e.Id == Id && (e.Status == 1 || e.Status == 2))
                        join user in _mstSleUserRepo.GetAll().Where(e => e.TypeUser == 3)
                        on funds.FundRaiserId equals user.Id
                        join fundPackage in _mstSleFundPackageRepo.GetAll() on user.FundPackageId equals fundPackage.Id

                        join funContent in _mstSleFundDetailContentRepo.GetAll()
                        on funds.Id equals funContent.FundId
                        select new GetFundsDetailByIdForUser
                        {
                            TitleFund = funds.FundTitle,
                            Created = user.Name,
                            FundRaisingDay = funds.FundRaisingDay,
                            FundName = funds.FundName,
                            PayFee = funds.IsPayFee == true ? "Trả phí cho người quyên góp" : "Không trả phí cho người quyên góp",
                            AmountOfMoney = funds.AmountOfMoney,
                            FinishFundRaising = funds.FundEndDate,
                            ReasonOfFund = funContent.ReasonCreatedFund,
                            ListImageUrl = _mstSleFundImageRepo.GetAll().AsNoTracking().Where(im => im.FundId == funds.Id).Select(im => im.ImageUrl).ToList(),
                            ContentFund = funContent.Content,
                            PaymenFee = fundPackage.PaymenFee,
                            IsPayeFee = funds.IsPayFee
                        }).FirstOrDefault();
            return fund;
        }

        public List<GetListFundOustandingDto> GetListFundOutStanding()
        {
            var listFundOutStanding = (from funDetail in _mstSleFundRepo.GetAll().Where(e => e.Status == 1 || e.Status == 2)
                                       join fundTopic in _mstSleFundTopictRepo.GetAll() on funDetail.Id equals fundTopic.FundId
                                       select new GetListFundOustandingDto
                                       {
                                           Id = (int)funDetail.Id,
                                           ImageUrl = funDetail.FundImageUrl,
                                           TopicOfFund = fundTopic.TopicName,
                                           FundRaisingDay = funDetail.FundRaisingDay,
                                           MainTitle = funDetail.FundTitle
                                       }).ToList();
            return listFundOutStanding;
        }

        public async Task<PagedResultDto<GetListFundPackageDto>> GetListFundPackage(FundPackageInputDto input)
        {
            var listFundPackage = from funPackage in _mstSleFundPackageRepo.GetAll().Where(e => e.Status == true)
                                  select new GetListFundPackageDto
                                  {
                                      Id = funPackage.Id,
                                      Discount = funPackage.Discount,
                                      PaymenFee = funPackage.PaymenFee,
                                      Description = funPackage.Description,
                                      Duration = funPackage.Duration,
                                      CreatedTime = funPackage.CreationTime
                                  };
            var totalCount = await listFundPackage.CountAsync();
            return new PagedResultDto<GetListFundPackageDto>
                (totalCount, await listFundPackage.PageBy(input).ToListAsync());
        }
        public async Task DonateForFund(DataDonateForFundInput input)
        {
            try
            {
                var commission = _mstSleFundRepo.FirstOrDefault(e => e.Id == input.FundId).Commission;

                //Trừ tiền người donate
                var accountUserDonate = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
                accountUserDonate.Balance -= input.AmountOfMoney;
                await _mstBankRepo.UpdateAsync(accountUserDonate);
                //Lưu giao dịch
                var receivedId = _mstSleFundRepo.FirstOrDefault(e => e.Id == input.FundId).FundRaiserId;
                var fundRaiserReceive = _mstSleUserRepo.FirstOrDefault(e => e.Id == receivedId).UserName;
                var userSend = _mstSleUserRepo.FirstOrDefault(e => e.Id == AbpSession.UserId).UserName;
                var transactionUserDonate = new FundTransactions();
                transactionUserDonate.FundId = input.FundId;
                transactionUserDonate.AmountOfMoney = input.AmountOfMoney;
                transactionUserDonate.MessageToFund = input.NoteTransaction;
                transactionUserDonate.Commission = input.AmountOfMoney * commission / 100;
                transactionUserDonate.Receiver = fundRaiserReceive;
                transactionUserDonate.Sender = userSend;
                await _mstSleFundTransactionRepo.InsertAsync(transactionUserDonate);


                //Nhận tiền và trừ tiền gói quỹ cho người tạo quỹ
                var fundRaiserId = _mstSleFundRepo.FirstOrDefault(e => e.Id == input.FundId).FundRaiserId;
                var accountUserRaiseFund = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == fundRaiserId);
                accountUserRaiseFund.Balance = input.AmountOfMoney - (input.AmountOfMoney * commission);
                await _mstBankRepo.UpdateAsync(accountUserRaiseFund);
                //Lưu giao dịch
               


                // Chuyển tiền cho admin
                var bankAdmin = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == 1);
                bankAdmin.Balance += input.AmountOfMoney * (commission / 100);
                await _mstBankRepo.UpdateAsync(accountUserRaiseFund);

               
                var transactionAdmin = new FundTransactions();
                transactionAdmin.FundId = input.FundId;
                transactionAdmin.AmountOfMoney = input.AmountOfMoney;
                transactionAdmin.MessageToFund = input.NoteTransaction;
                transactionAdmin.Commission = input.AmountOfMoney * commission / 100;
                transactionAdmin.Receiver = "Admin";
                transactionAdmin.Sender = userSend;
                await _mstSleFundTransactionRepo.InsertAsync(transactionUserDonate);
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi quyên góp");
            }



        }

        public float getTotalAmountDonateOfFund(int fundId)
        {
            var listTransactionOfFund = _mstSleFundTransactionRepo.GetAll().Where(e => e.FundId == fundId).ToList();
            float totalAmount = 0;
            foreach (var transaction in listTransactionOfFund)
            {
                totalAmount += transaction.AmountOfMoney;
            }
            return totalAmount;
        }

        public List<ListUserDonateForFundDto> GetListUserDonateForFund(int fundId)
        {
            var listUser = from transaction in _mstSleFundTransactionRepo.GetAll().Where(e => e.FundId == fundId)
                           join user in _mstSleUserRepo.GetAll() on transaction.UserId equals user.Id
                           join userAccount in _mstSleUserAccountRepo.GetAll() on user.Id equals userAccount.UserId
                           select new ListUserDonateForFundDto
                           {
                               UserName = userAccount.UserNameLogin,
                               UrlImage = user.ImageUrl,
                               AmountOfMoney = transaction.AmountOfMoney
                           };
            return listUser.ToList();
        }
        public async Task<List<ListFundPackageDto>> getListFundPackageForUserDonation()
        {
            var result = from fundPackage in _mstSleFundPackageRepo.GetAll().Where(e => e.Status == true)
                         select new ListFundPackageDto
                         {
                             Id = fundPackage.Id,
                             Name = fundPackage.PaymenFee.ToString() + fundPackage.Unit + "/" + fundPackage.Duration
                         };
            return await result.ToListAsync();
        }
        public async Task RegisterFundPackage(int fundPackage)
        {
            //var userCurrent = await _mstSleUserRepo.FirstOrDefaultAsync(e => e.Id == AbpSession.UserId);
            //userCurrent.FundPackageId = fundPackage;
            //await _mstSleUserRepo.UpdateAsync(userCurrent);
        }
        public void UpdatePermissionForFundRaiser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_appConfiguration.GetConnectionString("Default")))
            {

                connection.Execute(@"
                            INSERT INTO dbo.AbpPermissions (CreationTime, CreatorUserId, Discriminator, IsGranted, Name, TenantId ,RoleId,UserId) VALUES
                            (GetDate(),@p_userId, 'UserPermissionSetting',1,'Pages.FundRaising',1,4,@p_userId)
                        ", new
                {
                    p_userId = userId
                });

                connection.Execute(@"
                            INSERT INTO dbo.AbpPermissions (CreationTime, CreatorUserId, Discriminator, IsGranted, Name, TenantId,RoleId ,UserId) VALUES
                            (GetDate(),@p_userId, 'UserPermissionSetting',1,'Pages.FundRaising',1,4,@p_userId)
                        ", new
                {
                    p_userId = userId
                });

            }
        }
        public async Task<RegisterInforFundRaiserDto> GetForEditFundRaiser()
        {
            try
            {
                var fundRaiser = await _mstSleUserRepo.FirstOrDefaultAsync(e => e.Id == AbpSession.UserId && e.TypeUser == 3);
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
                var fundRaiser = await _mstSleUserRepo.FirstOrDefaultAsync(e => e.Id == AbpSession.UserId && e.TypeUser == 3);
                ObjectMapper.Map(input, fundRaiser);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<InforDetailBankAcountDto> GetInforBankUser()
        {
            try
            {
                var bankInfor = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
                if (bankInfor != null)
                {
                    var bank = new InforDetailBankAcountDto();
                    ObjectMapper.Map(bankInfor, bank);
                    return bank;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
        public async Task RegisterFundRaiser(RegisterInforFundRaiserDto input)
        {
            try
            {
                var user = _mstSleUserRepo.FirstOrDefault(e => e.Id == AbpSession.UserId);
                user.Address = input.Address;
                user.Company = input.Company;
                user.Country = input.Country;
                user.Phone = input.Phone;
                user.FundPackageId = 1;
                user.TypeUser = 3;
                await _mstSleUserRepo.UpdateAsync(user);
                using (SqlConnection connection = new SqlConnection(_appConfiguration.GetConnectionString("Default")))
                {

                    //connection.Execute(@"
                    //        INSERT INTO dbo.AbpPermissions (CreationTime, CreatorUserId, Discriminator, IsGranted, Name ,RoleId,UserId) VALUES
                    //        (GetDate(),@p_userId, 'RolePermissionSetting',1,'Pages',NULL,@p_userId)
                    //    ", new
                    //{
                    //    p_userId = AbpSession.UserId
                    //});

                    //connection.Execute(@"
                    //        INSERT INTO dbo.AbpPermissions (CreationTime, CreatorUserId, Discriminator, IsGranted, Name,RoleId ,UserId) VALUES
                    //        (GetDate(),@p_userId, 'UserPermissionSetting',1,'Pages.UserDonate',4,@p_userId)
                    //    ", new
                    //{
                    //    p_userId = AbpSession.UserId
                    //});

                    //connection.Execute(@"
                    //        INSERT INTO dbo.AbpPermissions (CreationTime, CreatorUserId, Discriminator, IsGranted, Name,RoleId ,UserId) VALUES
                    //        (GetDate(),@p_userId, 'RolePermissionSetting',1,'Pages.FundRaising',NULL,@p_userId)
                    //    ", new
                    //{
                    //    p_userId = AbpSession.UserId
                    //});

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task CreateOrEditAccountBank(InforDetailBankAcountDto input)
        {
            if (input.Id == null)
            {
                var bank = new BankAccount();
                ObjectMapper.Map(input, bank);
                bank.UserId = AbpSession.UserId;
                bank.Balance = 200;
                bank.Unit = "Coin";
                await _mstBankRepo.InsertAsync(bank);
            }
            else
            {
                var bankAccount = await _mstBankRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
                ObjectMapper.Map(input, bankAccount);
            }
        }
    }
}
