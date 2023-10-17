using Abp.Application.Services;
using esign.FundRaising.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.User
{
    public interface IUserFundRaising :IApplicationService
    {
        //Lấy ra danh sách các quỹ nổi bật
        List<GetListFundOustandingDto> GetListFundOutStanding();
        //Lấy ra thông tin các gói quỹ
        List<GetListFundPackageDto> GetListFundPackage();
        //Lấy ra thông tin chi tiết của quỹ theo id
        GetFundsDetailByIdForUser GetInforFundRaisingById();
        //Donate cho quỹ

        //Xem lại các quỹ mình đã donate

    }
}
