using esign.FundRaising.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class CreateOrEditFundRaisingDto
    {
        public string TitleFund { get; set; }
        public string Created { get; set; }
        public DateTime? FundRaisingDay { get; set; }
        public DetailFundContentDto ContentOfFund { get; set; }
        public GetInformationFundRaiserDto FundRaiser { get; set; }
    }
}
