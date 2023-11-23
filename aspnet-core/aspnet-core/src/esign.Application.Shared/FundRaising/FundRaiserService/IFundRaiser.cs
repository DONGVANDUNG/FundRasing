using esign.FundRaising.Admin.Dto;
using esign.FundRaising.FundRaiserService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising.FundRaiserService
{
    public interface IFundRaiser
    {
        //Đăng ký tài khoản gây quỹ ( gửi mail, để đăng nhập)

        //Update thông tin cá nhân
        //Task UpdateInformation(UpdateInformationFundRaiserDto input);

        //Tạo quỹ mới
        Task CreateFundRaising( CreateOrEditFundRaisingInputDto input);

        //Xem các giao dịch của quỹ
        Task<List<TransactionOfFundForDto>> getListTransactionForFund(int fundId);

        //Xem số lần cảnh cáo và nội dung cảnh cáo
        Task<List<UserWarningForDto>> getListWarningOfUser();
        //Gia hạn thời gian quỹ
        Task ExtendTimeOfFundRaising(DateTime timeExtend,int fundId);
        //Đóng quỹ ngay lập tức
        Task CloseFundRaising(int fundId);
        //Tạo tài khoản ví paypal

        //Sửa background cho quỹ

        //Sửa ảnh cho quỹ
        //Task UpdateImageUrlForFund(string imageUrl, int fundId);

        Task UpdateFundRaising(CreateOrEditFundRaisingDto input);

    }
}
