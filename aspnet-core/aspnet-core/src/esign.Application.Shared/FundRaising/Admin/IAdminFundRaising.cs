using Abp.Application.Services;
using esign.FundRaising.Admin.Dto;
using esign.FundRaising.FundRaiserService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin
{
    public interface IAdminFundRaising : IApplicationService
    {
        //Lấy ra thông tin tất cả các tài khoản
        List<UserAccountForViewDto> getListUserAccount();
        //Cảnh cáo tài khoản
        void WarningAccountUser(string contentWarning);
        //Lấy ra thông tin tất cả các người gây quỹ
        List<GetInformationFundRaiserDto> getListFundRaiser();
        //Lấy ra thông tin tất cả các quỹ
        List<GetFundRaisingViewForAdminDto> getListFundRaising();
        //Lấy ra thông tin tất cả các giao dịch theo quỹ
        TransactionOfFundForDto getListTransactionForFund(int fundId);
    }
}
