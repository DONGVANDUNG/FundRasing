using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using esign.Enitity;
using esign.Entity;
using esign.FundRaising.UserFundRaising.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    public class UserAppService : esignAppServiceBase, IUserFundRaising
    {
        private readonly IRepository<Funds,int> _mstSleFundRepo;
        private readonly IRepository<FundRaiser, int> _mstSleFundRaiserRepo;
        private readonly IRepository<FundDetailContent, int> _mstSleFundDetailContentRepo;
        private readonly IRepository<FundRaisingTopic, int> _mstSleFundTopictRepo;
        private readonly IRepository<FundPackage, int> _mstSleFundPackageRepo;

        public UserAppService(IRepository<Funds> mstSleFundRepo, IRepository<FundRaiser, int>
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
            var listFundOutStanding = (from funDetail in _mstSleFundRepo.GetAll().Where(e=> e.Status == 1 || e.Status == 2)
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
            var listFundPackage = from funPackage in _mstSleFundPackageRepo.GetAll().Where(e=> e.Status == true)
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
    }
}
