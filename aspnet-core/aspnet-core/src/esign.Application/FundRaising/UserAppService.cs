﻿using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Dapper;
using esign.Authorization.Users;
using esign.Configuration;
using esign.Enitity;
using esign.Entity;
using esign.FundRaising.UserFundRaising.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using Newtonsoft.Json;
using NUglify.Html;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace esign.FundRaising
{
    public class UserAppService : esignAppServiceBase, IUserFundRaising
    {
        private readonly IRepository<Funds, long> _mstSleFundRepo;
        private readonly IRepository<FundRaiser, long> _mstSleFundRaiserRepo;
        private readonly IRepository<FundDetailContent, long> _mstSleFundDetailContentRepo;
        private readonly IRepository<FundRaisingTopic, int> _mstSleFundTopictRepo;
        private readonly IRepository<FundPackage, int> _mstSleFundPackageRepo;
        private readonly IRepository<FundTransactions, int> _mstSleFundTransactionRepo;
        private readonly IRepository<UserAccount, int> _mstSleUserAccountRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<FundImage, long> _mstSleFundImageRepo;
        private readonly IConfigurationRoot _appConfiguration;

        public UserAppService(IRepository<Funds, long> mstSleFundRepo, IRepository<FundRaiser, long>
            mstSleFundRaiserRepo, IRepository<FundDetailContent, long> mstSleFundDetailContentRepo,
            IWebHostEnvironment hostingEnvironment, IWebHostEnvironment env,
            IRepository<FundRaisingTopic, int> mstSleFundTopictRepo,
            IRepository<FundPackage, int> mstSleFundPackageRepo,
            IRepository<FundTransactions, int> mstSleFundTransactionRepo,
            IRepository<UserAccount, int> mstSleUserAccountRepo,
            IRepository<User, long> mstSleUserRepo, IRepository<FundImage, long> mstSleFundImageRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundDetailContentRepo = mstSleFundDetailContentRepo;
            _mstSleFundTopictRepo = mstSleFundTopictRepo;
            _mstSleFundPackageRepo = mstSleFundPackageRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserAccountRepo = mstSleUserAccountRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _appConfiguration = env.GetAppConfiguration();
            _appConfiguration = hostingEnvironment.GetAppConfiguration();
            _mstSleFundImageRepo = mstSleFundImageRepo;
        }
        public GetFundsDetailByIdForUser GetInforFundRaisingById(long Id)
        {
            var fund = (from funds in _mstSleFundRepo.GetAll().Where(e => e.Id == Id && e.Status == 1 || e.Status == 2)
                        join fundRaiser in _mstSleFundRaiserRepo.GetAll() on funds.FundRaiserId equals fundRaiser.Id
                        join funContent in _mstSleFundDetailContentRepo.GetAll() on funds.Id equals funContent.FundId
                        //join fundImage in _mstSleFundImageRepo.GetAll() on funds.Id equals fundImage.FundId
                        select new GetFundsDetailByIdForUser
                        {
                            TitleFund = funds.FundTitle,
                            Created = fundRaiser.Name,
                            FundRaisingDay = funds.FundRaisingDay,
                            FundName = funds.FundName,
                            IsPayFee = funds.IsPayFee == true ? "Trả phí cho người quyên góp" : "Không trả phí cho người quyên góp",
                            AmountOfMoney = funds.AmountOfMoney.ToString(),
                            FinishFundRaising = funds.FundEndDate,
                            ReasonOfFund = funContent.ReasonCreatedFund,
                            ListImageUrl = _mstSleFundImageRepo.GetAll().AsNoTracking().Where(im=>im.FundId == funds.Id).Select(im=>im.ImageUrl).ToList(),
                            ContentFund = funContent.Content,
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
            using (HttpClient client = new HttpClient())
            {
                var inputData = new DetailDonateForFundDto();
                List<Items> listItem = new List<Items>();
                SenderBatchHeader senderBatchHeader = new SenderBatchHeader();
                Items item = new Items();
                Amount amount = new Amount();
                amount.value = input.AmountOfMoney;
                amount.currency = "USD";
                item.recipient_type = "Email";
                item.amount = amount;
                senderBatchHeader.recipient_type = "Email";
                inputData.sender_batch_header = senderBatchHeader;

                var fundRaiserCreatedFund = _mstSleFundRepo.FirstOrDefault(e => e.Id == input.FundId).FundRaiserId;

                var emailFund = _mstSleFundRaiserRepo.FirstOrDefault(e => e.Id == fundRaiserCreatedFund).Email;
                item.receiver = emailFund;
                listItem.Add(item);
                inputData.items = listItem;

                string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(inputData);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "A21AAIejJmmgDMGn5fvV2moex46BPvhlKuD6jxFV014HPI62E8zHzNv7tE1GqvHmsFQrBSDhSdqXZ7I5y8o8trCdfCHQ81o3g");
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://api.sandbox.paypal.com/v1/payments/payouts", content);
                if (response.IsSuccessStatusCode == true)
                {
                    var detailTransaction = new ResultResponseDonatedDto();
                    string responseContent = await response.Content.ReadAsStringAsync();
                    detailTransaction = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultResponseDonatedDto>(responseContent);
                    var transaction = new FundTransactions();
                    transaction.AmountOfMoney = input.AmountOfMoney;
                    transaction.EmailReceiver = emailFund;
                    transaction.MessageToFund = input.NoteTransaction;
                    transaction.FundId = input.FundId;
                    //transaction.TransactionCode = detailTransaction.batch_header.payout_batch_id;
                    await _mstSleFundTransactionRepo.InsertAsync(transaction);
                }
                else
                {
                    throw new UserFriendlyException("Có lỗi xảy ra. Hãy thử lại!");
                }

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
            var userCurrent = await _mstSleUserRepo.FirstOrDefaultAsync(e => e.Id == AbpSession.UserId);
            userCurrent.FundPackageId = fundPackage;
            await _mstSleUserRepo.UpdateAsync(userCurrent);
        }
        public void UpdatePermissionForFundRaiser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_appConfiguration.GetConnectionString("Default")))
            {

                connection.Execute(@"
                            INSERT INTO dbo.AbpPermissions (CreationTime, CreatorUserId, Discriminator, IsGranted, Name, TenantId ,RoleId,UserId) VALUES
                            (GetDate(),@p_userId, 'UserPermissionSetting',1,'Pages',1,2,@p_userId)
                        ", new
                {
                    p_userId = userId
                });

                connection.Execute(@"
                            INSERT INTO dbo.AbpPermissions (CreationTime, CreatorUserId, Discriminator, IsGranted, Name, TenantId ,UserId) VALUES
                            (GetDate(),@p_userId, 'UserPermissionSetting',1,'Pages.UserDonate',1,2,@p_userId)
                        ", new
                {
                    p_userId = userId
                });

            }
        }
    }
}
