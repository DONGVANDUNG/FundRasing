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
        public string PayFee { get; set; }
        public DateTime? FundRaisingDay { get; set; }
        public DateTime? FinishFundRaising { get; set; }
        public List<string> ListImageUrl { get; set; }
        public string ReasonOfFund { get; set; }
        public string Description { get; set; }
        public float? AmountOfMoney { get; set; }
        public string ContentFund { get; set; }
        public float? PaymenFee { get; set; }
        public bool IsPayeFee { get; set; }
    }
}
