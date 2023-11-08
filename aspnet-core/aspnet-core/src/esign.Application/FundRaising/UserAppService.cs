using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using esign.Authorization.Users;
using esign.Enitity;
using esign.Entity;
using esign.FundRaising.UserFundRaising.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        private readonly IRepository<Funds, int> _mstSleFundRepo;
        private readonly IRepository<FundRaiser, int> _mstSleFundRaiserRepo;
        private readonly IRepository<FundDetailContent, int> _mstSleFundDetailContentRepo;
        private readonly IRepository<FundRaisingTopic, int> _mstSleFundTopictRepo;
        private readonly IRepository<FundPackage, int> _mstSleFundPackageRepo;
        private readonly IRepository<FundTransactions, int> _mstSleFundTransactionRepo;
        private readonly IRepository<UserAccount, int> _mstSleUserAccountRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;

        public UserAppService(IRepository<Funds> mstSleFundRepo, IRepository<FundRaiser, int>
            mstSleFundRaiserRepo, IRepository<FundDetailContent, int> mstSleFundDetailContentRepo,
            IRepository<FundRaisingTopic, int> mstSleFundTopictRepo,
            IRepository<FundPackage, int> mstSleFundPackageRepo,
            IRepository<FundTransactions, int> mstSleFundTransactionRepo,
            IRepository<UserAccount, int> mstSleUserAccountRepo,
            IRepository<User, long> mstSleUserRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundDetailContentRepo = mstSleFundDetailContentRepo;
            _mstSleFundTopictRepo = mstSleFundTopictRepo;
            _mstSleFundPackageRepo = mstSleFundPackageRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserAccountRepo = mstSleUserAccountRepo;
            _mstSleUserRepo = mstSleUserRepo;
        }
        public GetFundsDetailByIdForUser GetInforFundRaisingById(int Id)
        {
            var fund = (from funDetail in _mstSleFundRepo.GetAll().Where(e => e.Id == Id && e.Status == 1 || e.Status == 2)
                        join fundRaiser in _mstSleFundRaiserRepo.GetAll() on funDetail.FundRaiserId equals fundRaiser.Id
                        join funContent in _mstSleFundDetailContentRepo.GetAll() on funDetail.FundContentId equals funContent.Id
                        select new GetFundsDetailByIdForUser
                        {
                            TitleFund = funDetail.FundTitle,
                            Created = fundRaiser.Name,
                            FundRaisingDay = funDetail.FundRaisingDay,
                            ContentOfFund = (new DetailFundContentDto
                            {
                                Id = Id,
                                Header = funContent.Header,
                                ReasonCreatedFund = funContent.ReasonCreatedFund,
                                IdeaCreadtedFund = funContent.IdeaCreadtedFund,
                                Footer = funContent.Footer
                            }),
                        }).FirstOrDefault();
            return fund;
        }

        public List<GetListFundOustandingDto> GetListFundOutStanding()
        {
            var listFundOutStanding = (from funDetail in _mstSleFundRepo.GetAll().Where(e => e.Status == 1 || e.Status == 2)
                                       join fundTopic in _mstSleFundTopictRepo.GetAll() on funDetail.Id equals fundTopic.FundId
                                       select new GetListFundOustandingDto
                                       {
                                           Id = funDetail.Id,
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
        public async Task DonateForFund(DetailDonateForFundDto input, int fundId)
        {
            using (HttpClient client = new HttpClient())
            {

                string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(input);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "A21AALaCMoEq7nEt65LBp6qbQBhGTgYTraDNKiX8v-w0Vo1391ABpYsPul2elML2iibDn6qJa-1vQuj6n--uZPumniEDFK9mw");
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://api.sandbox.paypal.com/v1/payments/payouts", content);
                if (response.IsSuccessStatusCode == true)
                {
                    var detailTransaction = new ResultResponseDonatedDto();
                    string responseContent = await response.Content.ReadAsStringAsync();
                    detailTransaction = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultResponseDonatedDto>(responseContent);
                    var transaction = new FundTransactions();
                    transaction.AmountOfMoney = input.items[0].amount.value;
                    transaction.EmailReceiver = input.sender_batch_header.recipient_type;
                    transaction.MessageToFund = input.items[0].note;
                    transaction.FundId = fundId;
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
    }
}
