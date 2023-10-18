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
        public DateTime? FundRaisingDay { get; set; }
        public DetailFundContentDto ContentOfFund { get; set; }
        public GetInformationFundRaiserDto FundRaiser { get; set; }

    }
}
