using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Dapper;
using esign.Authorization.Users;
using esign.Configuration;
using esign.Enitity;
using esign.Entity;
using esign.FundRaising.Admin.Dto;
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
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<FundImage, long> _mstSleFundImageRepo;
        private readonly IRepository<BankAccount, long> _mstBankRepo;
        private readonly IRepository<RequestToFundRaiser, long> _mstRequestToFundRaiserRepo;
        private readonly IConfigurationRoot _appConfiguration;


        public UserAppService(IRepository<Funds, long> mstSleFundRepo,
            //IRepository<FundRaiser, long>
            // mstSleFundRaiserRepo,
            IRepository<FundDetailContent, long> mstSleFundDetailContentRepo,
            IWebHostEnvironment hostingEnvironment, IWebHostEnvironment env,
            IRepository<FundRaisingTopic, int> mstSleFundTopictRepo,
            IRepository<FundPackage, int> mstSleFundPackageRepo,
            IRepository<FundTransactions, int> mstSleFundTransactionRepo,
            IRepository<User, long> mstSleUserRepo, IRepository<FundImage, long> mstSleFundImageRepo, IRepository<BankAccount, long> mstBankRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            /// _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundDetailContentRepo = mstSleFundDetailContentRepo;
            _mstSleFundTopictRepo = mstSleFundTopictRepo;
            _mstSleFundPackageRepo = mstSleFundPackageRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _appConfiguration = env.GetAppConfiguration();
            _appConfiguration = hostingEnvironment.GetAppConfiguration();
            _mstSleFundImageRepo = mstSleFundImageRepo;
            _mstBankRepo = mstBankRepo;
        }
        //public GetFundsDetailByIdForUser GetInforFundRaisingById(long Id)
        //{
        //    var fund = (from funds in _mstSleFundRepo.GetAll().Where(e => e.Id == Id && (e.Status == 1 || e.Status == 2))
        //                join user in _mstSleUserRepo.GetAll().Where(e => e.TypeUser == 3)
        //                on funds.FundRaiserId equals user.Id
        //                join fundPackage in _mstSleFundPackageRepo.GetAll() on user.FundPackageId equals fundPackage.Id

        //                join funContent in _mstSleFundDetailContentRepo.GetAll()
        //                on funds.Id equals funContent.FundId
        //                select new GetFundsDetailByIdForUser
        //                {
        //                    Id = funds.Id,
        //                    TitleFund = funds.FundTitle,
        //                    Created = user.Name,
        //                    FundRaisingDay = funds.FundRaisingDay,
        //                    FundName = funds.FundName,
        //                    PayFee = funds.IsPayFee == true ? "Trả phí cho người quyên góp" : "Không trả phí cho người quyên góp",
        //                    AmountOfMoney = funds.AmountOfMoney,
        //                    FinishFundRaising = funds.FundEndDate,
        //                    ReasonOfFund = funContent.ReasonCreatedFund,
        //                    ListImageUrl = _mstSleFundImageRepo.GetAll().AsNoTracking().Where(im => im.FundId == funds.Id).Select(im => im.ImageUrl).ToList(),
        //                    ContentFund = funContent.Content,
        //                    PaymenFee = fundPackage.PaymenFee,
        //                    IsPayeFee = funds.IsPayFee
        //                }).FirstOrDefault();
        //    return fund;
        //}

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

        public async Task<List<GetListFundPackageDto>> GetListFundPackage(FundPackageInputDto input)
        {
            var listFundPackage = from funPackage in _mstSleFundPackageRepo.GetAll().Where(e => e.Status == true)
                                  select new GetListFundPackageDto
                                  {
                                      Id = funPackage.Id,
                                      //Discount = funPackage.Discount,
                                      Commission = funPackage.Commission,
                                      PaymenFee = funPackage.PaymenFee,
                                      Description = funPackage.Description,
                                      Duration = funPackage.Duration,
                                      CreatedTime = funPackage.CreationTime
                                  };
            return await listFundPackage.ToListAsync();
        }
        public async Task DonateForFund(DataDonateForFundInput input)
        {
            try
            {
                var userCreatedFundId = _mstSleFundRepo.FirstOrDefault(e => e.Id == input.FundId).FundRaiserId;
                var fundPackageId = _mstSleUserRepo.FirstOrDefault(e => e.Id == userCreatedFundId).FundPackageId;
                var commission = _mstSleFundPackageRepo.FirstOrDefault(e=>e.Id == fundPackageId).Commission;

                //Trừ tiền người donate
                var accountUserDonate = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
                accountUserDonate.Balance -= input.AmountOfMoney;
                await _mstBankRepo.UpdateAsync(accountUserDonate);

                var fundRaiserId = _mstSleFundRepo.FirstOrDefault(e => e.Id == input.FundId).FundRaiserId;
                var fundRaiserReceive = _mstSleUserRepo.FirstOrDefault(e => e.Id == fundRaiserId).UserName;
                var userSend = _mstSleUserRepo.FirstOrDefault(e => e.Id == AbpSession.UserId).UserName;

                
                //Nhận tiền
                var accountUserRaiseFund = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == fundRaiserId);
                accountUserRaiseFund.Balance += input.AmountOfMoney;
                await _mstBankRepo.UpdateAsync(accountUserRaiseFund);

                var transactionUserDonate = new FundTransactions();
                transactionUserDonate.FundId = input.FundId;
                transactionUserDonate.AmountOfMoney = input.AmountOfMoney;
                transactionUserDonate.MessageToFund = input.NoteTransaction;
                transactionUserDonate.Commission = input.AmountOfMoney * commission / 100;
                transactionUserDonate.Receiver = fundRaiserReceive;
                transactionUserDonate.Sender = userSend;
                transactionUserDonate.SenderId = AbpSession.UserId;
                transactionUserDonate.ReceiverId = fundRaiserId;
                transactionUserDonate.Balance = accountUserRaiseFund.Balance;
                transactionUserDonate.IsPublic = input.IsPublic;
                await _mstSleFundTransactionRepo.InsertAsync(transactionUserDonate);
                

                //Lưu giao dịch


                // Chuyển tiền cho admin
                var bankAdmin = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == 1);
                bankAdmin.Balance += input.AmountOfMoney * (commission / 100);
                await _mstBankRepo.UpdateAsync(accountUserRaiseFund);

                var transactionAdmin = new FundTransactions();
                transactionAdmin.FundId = input.FundId;
                transactionAdmin.AmountOfMoney = input.AmountOfMoney * (commission / 100);
                transactionAdmin.MessageToFund = "Trừ tiền phí gói quỹ";
                transactionAdmin.Commission = input.AmountOfMoney * commission / 100;
                transactionAdmin.Receiver = "Admin";
                transactionAdmin.Sender = userSend;
                transactionAdmin.SenderId = fundRaiserId;
                transactionAdmin.Balance = bankAdmin.Balance;
                transactionAdmin.ReceiverId = 1;

                await _mstSleFundTransactionRepo.InsertAsync(transactionAdmin);
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
                           select new ListUserDonateForFundDto
                           {
                               //UserName = userAccount.UserNameLogin,
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
                //user.TypeUser = 3;
                RequestToFundRaiser request = new RequestToFundRaiser();
                request.UserId = AbpSession.UserId;
                request.RequestTime = DateTime.Now;
                request.IsApprove = false;
                await _mstRequestToFundRaiserRepo.InsertAsync(request);

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
            if (input.Id == 0)
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

        //public async Task<List<GetFundRaisingViewForAdminDto>> getListFundRaising()
        //{
        //    var listFundRaising = (from fundRaising in _mstSleFundRepo.GetAll()
        //                           join user in _mstSleUserRepo.GetAll() on fundRaising.FundRaiserId equals user.Id
        //                           select new GetFundRaisingViewForAdminDto
        //                           {
        //                               Id = (int)fundRaising.Id,
        //                               FundName = fundRaising.FundName,
        //                               FundFinishDay = fundRaising.FundRaisingDay,
        //                               FundRaisingDay = fundRaising.FundRaisingDay,
        //                               AmountOfMoney = fundRaising.AmountOfMoney,
        //                               Status = fundRaising.Status == 3 ? "Đã đóng" : "Đang hoạt động",
        //                               ListImageUrl = _mstSleFundImageRepo.GetAll().Where(e => e.FundId == fundRaising.Id).Select(e => e.ImageUrl).ToList(),
        //                               FundStartDate = fundRaising.FundRaisingDay,
        //                               FundTitle = fundRaising.FundTitle,
        //                               FundRaiser = user.UserName
        //                           }).ToListAsync();
        //    return await listFundRaising;
        //}
    }
}
