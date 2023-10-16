using esign.FundRaising.FundRaiser.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiser
{
    public interface IFundRaiser
    {
        //Đăng ký tài khoản gây quỹ ( gửi mail, để đăng nhập)
        bool RegisterAccountFundRaising();

        //Update thông tin cá nhân
        bool UpdateInformation(GetInformationFundRaiserDto information);

        //Tạo quỹ mới
        bool CreateFundRaising(GetInformationFundRaiserDto information);

        //Xem các giao dịch của quỹ
        List<TransactionOfFundForDto> getListTransactionForFund(int fundId);

        //Xem số lần cảnh cáo và nội dung cảnh cáo
        List<UserWarningForDto> getListWarningOfUser();
        //Gia hạn thời gian quỹ
        void ExtendTimeOfFundRaising(DateTime timeExtend);
        //Đóng quỹ ngay lập tức
        void CloseFundRaising();
        //Tạo tài khoản ví paypal

        //Sửa background cho quỹ

        //Sửa ảnh cho quỹ
        void UpdateImageUrlForFund(string iamgeUrl);
    }
}
