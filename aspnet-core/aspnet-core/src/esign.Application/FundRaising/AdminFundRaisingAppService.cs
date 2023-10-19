using Abp.Domain.Repositories;
using esign.Enitity;
using esign.Entity;
using esign.FundRaising.Admin;
using esign.FundRaising.Admin.Dto;
using esign.FundRaising.FundRaiserService.Dto;
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
        public AdminFundRaisingAppService(IRepository<Funds> mstSleFundRepo, IRepository<FundRaiser, int>
            mstSleFundRaiserRepo, IRepository<FundDetailContent, int> mstSleFundDetailContentRepo,
            IRepository<FundRaisingTopic, int> mstSleFundTopictRepo,
            IRepository<FundPackage, int> mstSleFundPackageRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundDetailContentRepo = mstSleFundDetailContentRepo;
            _mstSleFundTopictRepo = mstSleFundTopictRepo;
            _mstSleFundPackageRepo = mstSleFundPackageRepo;
        }
        public List<GetInformationFundRaiserDto> getListFundRaiser()
        {
            throw new NotImplementedException();
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
