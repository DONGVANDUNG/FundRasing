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
        GetFundsDetailByIdForUser GetInforFundRaisingById(long Id);
        //Donate cho quỹ
        //void DonateForFund();
        Task DonateForFund(DataDonateForFundInput input);
        //Xem lại các quỹ mình đã donate

        // Lấy ra tổng số tiền donate của quỹ đó
        float getTotalAmountDonateOfFund(int fundId);
        //Lấy ra danh sách những người donate cho quỹ
        List<ListUserDonateForFundDto> GetListUserDonateForFund(int fundId);

        Task<List<ListFundPackageDto>> getListFundPackageForUserDonation();

        void UpdatePermissionForFundRaiser(int userId);
    }
}
