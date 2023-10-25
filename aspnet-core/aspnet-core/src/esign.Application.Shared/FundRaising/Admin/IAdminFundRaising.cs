using Abp.Application.Services;
using Abp.Application.Services.Dto;
using esign.FundRaising.Admin.Dto;
using esign.FundRaising.FundRaiserService.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising.Admin
{
    public interface IAdminFundRaising : IApplicationService
    {
        //Lấy ra thông tin tất cả các tài khoản
        Task<List<UserAccountForViewDto>> getListUserAccount();
        //Cảnh cáo tài khoản
        void WarningAccountUser(string contentWarning, int userId);
        //Lấy ra thông tin tất cả các người gây quỹ
        Task<PagedResultDto<GetInformationFundRaiserDto>> getListFundRaiser(GetAllFundRaiserForInputDto input);
        //Lấy ra thông tin tất cả các quỹ
        Task<List<GetFundRaisingViewForAdminDto>> getListFundRaising();
        //Lấy ra thông tin tất cả các giao dịch theo quỹ
        Task<List<TransactionOfFundForDto>> getListTransactionForFund(int fundId);
    }
}
