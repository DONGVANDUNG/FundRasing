using Abp.Application.Services.Dto;
using esign.FundRaising.FundRaiserService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising
{
    public class GetFundsDetailByIdForUser : EntityDto<long>
    {
        public string TitleFund { get; set; }
        public string Created { get; set; }
        public string FundName { get; set; }
        public string IsPayFee { get; set; }
        public DateTime? FundRaisingDay { get; set; }
        public DateTime? FinishFundRaising { get; set; }
        public string ImageUrl { get; set; }
        public string ReasonOfFund { get; set; }
        public string Description { get; set; }
        public string AmountOfMoney { get; set; }

    }
}
