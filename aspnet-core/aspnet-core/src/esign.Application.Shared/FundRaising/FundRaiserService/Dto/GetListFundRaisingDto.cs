﻿using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class GetListFundRaisingDto
    {
        public long? Id { get; set; }
        public string FundRaisingDay { get; set; }
        public string FundName { get; set; }
        public string FundEndDate { get; set; }
        public decimal? AmountDonationTarget { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }
        public string FundRaiser { get; set; }
        public string FundTitle { get; set; }
        public int? TotalPost { get; set; }
        public int? TotalDonate { get; set; }
    }
}
