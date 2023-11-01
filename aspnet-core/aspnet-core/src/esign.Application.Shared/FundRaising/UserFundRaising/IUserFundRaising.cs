using Abp.Application.Services;
using Abp.Application.Services.Dto;
using esign.FundRaising.UserFundRaising.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    public interface IUserFundRaising :IApplicationService
    {
        //Lấy ra danh sách các quỹ nổi bật
        List<GetListFundOustandingDto> GetListFundOutStanding();
        //Lấy ra thông tin các gói quỹ
        Task<PagedResultDto<GetListFundPackageDto>> GetListFundPackage(FundPackageInputDto input);
        //Lấy ra thông tin chi tiết của quỹ theo id
        GetFundsDetailByIdForUser GetInforFundRaisingById(int Id);
        //Donate cho quỹ
        //void DonateForFund();

        //Xem lại các quỹ mình đã donate
    }
}
