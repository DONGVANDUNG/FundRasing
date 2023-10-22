using Abp.Domain.Repositories;
using esign.Authorization.Users;
using esign.Enitity;
using esign.Entity;
using esign.FundRaising.Admin;
using esign.FundRaising.Admin.Dto;
using esign.FundRaising.FundRaiserService.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace esign.FundRaising
{
    public class AdminFundRaisingAppService : esignAppServiceBase, IAdminFundRaising
    {
        private readonly IRepository<Funds, int> _mstSleFundRepo;
        private readonly IRepository<FundRaiser, int> _mstSleFundRaiserRepo;
        private readonly IRepository<FundDetailContent, int> _mstSleFundDetailContentRepo;
        private readonly IRepository<FundRaisingTopic, int> _mstSleFundTopictRepo;
        private readonly IRepository<FundPackage, int> _mstSleFundPackageRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<UserAccount, int> _mstSleUserAccountRepo;
        private readonly IRepository<FundTransactions, int> _mstSleTransactionRepo;
        private readonly IRepository<UserWarning, int> _mstSleUserWarningRepo;


        public AdminFundRaisingAppService(IRepository<Funds> mstSleFundRepo, IRepository<FundRaiser, int>
            mstSleFundRaiserRepo, IRepository<FundDetailContent, int> mstSleFundDetailContentRepo,
            IRepository<FundRaisingTopic, int> mstSleFundTopictRepo,
            IRepository<FundPackage, int> mstSleFundPackageRepo, IRepository<User, long> mstSleUserRepo, IRepository<UserAccount, int> mstSleUserAccountRepo, IRepository<FundTransactions, int> mstSleTransactionRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundDetailContentRepo = mstSleFundDetailContentRepo;
            _mstSleFundTopictRepo = mstSleFundTopictRepo;
            _mstSleFundPackageRepo = mstSleFundPackageRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _mstSleUserAccountRepo = mstSleUserAccountRepo;
            _mstSleTransactionRepo = mstSleTransactionRepo;
        }
        public async Task<List<GetInformationFundRaiserDto>> getListFundRaiser()
        {
            var listFundRaiser = (from user in _mstSleUserRepo.GetAll().Where(e => e.TypeUser == 2)
                                  join fundRaising in _mstSleFundRaiserRepo.GetAll() on user.Id equals fundRaising.UserId
                                  join userAccount in _mstSleUserAccountRepo.GetAll() on user.Id equals userAccount.UserId
                                  select new GetInformationFundRaiserDto
                                  {
                                      Id = fundRaising.Id,
                                      Description = fundRaising.Introduce,
                                      Name = fundRaising.Name,
                                      AccountLogin = userAccount.UserNameLogin,
                                      Position = fundRaising.Position,
                                      StatusAccount = userAccount.Status == true ? "Active" : "Not Active",
                                      ImageUrl = user.ImageUrl
                                  }).ToListAsync();
            return await listFundRaiser;
        }

        public async Task<List<GetFundRaisingViewForAdminDto>> getListFundRaising()
        {
            var listFundRaising = (from fundRaising in _mstSleFundRepo.GetAll()
                                   join funRaiser in _mstSleFundRaiserRepo.GetAll() on fundRaising.FundRaiserId equals funRaiser.Id
                                   select new GetFundRaisingViewForAdminDto
                                   {
                                       Id = fundRaising.Id,
                                       FundFinishDay = fundRaising.FundRaisingDay,
                                       FundRaiser = funRaiser.Name,
                                       AmountOfMoney = fundRaising.AmountOfMoney,
                                       Status = fundRaising.Status == 3 ? "Đã đóng" : "Đang hoạt động"
                                   }).ToListAsync();
            return await listFundRaising;
        }

        public async Task<List<TransactionOfFundForDto>> getListTransactionForFund(int fundId)
        {
            var listTransaction = (from transaction in _mstSleTransactionRepo.GetAll().Where(e => e.FundId == fundId)
                                   join user in _mstSleUserRepo.GetAll() on transaction.UserId equals user.Id
                                   join fund in _mstSleFundRepo.GetAll() on transaction.FundId equals fund.Id
                                   select new TransactionOfFundForDto
                                   {
                                       Id = transaction.Id,
                                       Amount = transaction.AmountOfMoney,
                                       Content = transaction.MessageToFund,
                                       FundName = fund.FundName,
                                       Sender = user.UserLogin
                                   }).ToListAsync();
            return await listTransaction;
        }

        public async Task<List<UserAccountForViewDto>> getListUserAccount()
        {
            var listAccount = (from account in _mstSleUserAccountRepo.GetAll()
                               join user in _mstSleUserRepo.GetAll() on account.FundRaiserId equals user.Id
                               select new UserAccountForViewDto
                               {
                                   Id = account.Id,
                                   Email = user.Email,
                                   LevelWarning = account.LevelWarning == 1 ? "Cảnh cáo" : account.LevelWarning == 2 ? "Báo động" : "Khóa",
                                   UserName = user.UserName,
                                   UserNameLogin = user.UserLogin
                               }).ToListAsync();
            return await listAccount;
        }

        public async void WarningAccountUser(string contentWarning, int userId)
        {
            var user = await _mstSleUserWarningRepo.FirstOrDefaultAsync(e => e.Id == userId);
            var accountUser = await _mstSleUserAccountRepo.FirstOrDefaultAsync(e => e.UserId == userId);
            accountUser.LevelWarning += 1;
            await _mstSleUserAccountRepo.UpdateAsync(accountUser);
            user.ContentWarning = contentWarning;
            user.LevelWarning += 1;
            await _mstSleUserWarningRepo.UpdateAsync(user);
        }
    }
}
