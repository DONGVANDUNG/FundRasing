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


        public AdminFundRaisingAppService(IRepository<Funds> mstSleFundRepo, IRepository<FundRaiser, int>
            mstSleFundRaiserRepo, IRepository<FundDetailContent, int> mstSleFundDetailContentRepo,
            IRepository<FundRaisingTopic, int> mstSleFundTopictRepo,
            IRepository<FundPackage, int> mstSleFundPackageRepo, IRepository<User, long> mstSleUserRepo, IRepository<UserAccount, int> mstSleUserAccountRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundDetailContentRepo = mstSleFundDetailContentRepo;
            _mstSleFundTopictRepo = mstSleFundTopictRepo;
            _mstSleFundPackageRepo = mstSleFundPackageRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _mstSleUserAccountRepo = mstSleUserAccountRepo;
        }
        public async Task<List<GetInformationFundRaiserDto>> getListFundRaiser()
        {
            var listFundRaiser = (from user in _mstSleUserRepo.GetAll().Where(e => e.TypeUser == 2)
                                  join fundRaising in _mstSleFundRaiserRepo.GetAll() on user.Id  equals fundRaising.UserId
                                  join userAccount in _mstSleUserAccountRepo.GetAll() on user.Id equals userAccount.UserId
                                  select new GetInformationFundRaiserDto
                                  {
                                      Id = fundRaising.Id,
                                      Description = fundRaising.Introduce,
                                      Name = fundRaising.Name,
                                      AccountLogin = userAccount.UserNameLogin,
                                      Position = fundRaising.Position,
                                      StatusAccount = userAccount.Status == true ? "Active": "Not Active",
                                      ImageUrl = user.ImageUrl
                                  }).ToListAsync();
            return await listFundRaiser;
        }

        public List<GetFundRaisingViewForAdminDto> getListFundRaising()
        {
            throw new NotImplementedException();
        }

        public TransactionOfFundForDto getListTransactionForFund(int fundId)
        {
            throw new NotImplementedException();
        }

        public List<UserAccountForViewDto> getListUserAccount()
        {
            throw new NotImplementedException();
        }

        public void WarningAccountUser(string contentWarning)
        {
            throw new NotImplementedException();
        }
    }
}
