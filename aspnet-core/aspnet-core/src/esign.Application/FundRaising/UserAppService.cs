using Abp.Domain.Repositories;
using esign.FundRaising.User;
using esign.FundRaising.User.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    public class UserAppService : esignAppServiceBase, IUserFundRaising
    {
        private readonly IRepository<Funds,int> _mstSleFundRepo;
        private readonly IRepository<FundRaisers, int> _mstSleFundRaiserRepo;

        public UserAppService(IRepository<Funds> mstSleFundRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
        }
        public GetFundsDetailByIdForUser GetInforFundRaisingById(int Id)
        {
            var result = from funDetail in _mstSleFundRepo.GetAll().Where(e=>e.Id == Id) 
                         left join 
                select new GetFundsDetailByIdForUser
                {
                    TitleFund = funDetail.FundTitle,
                    Created
                })
                return result.ToList();
        }

        public List<GetListFundOustandingDto> GetListFundOutStanding()
        {
            throw new NotImplementedException();
        }

        public List<GetListFundPackageDto> GetListFundPackage()
        {
            throw new NotImplementedException();
        }
    }
}
