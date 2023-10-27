using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising
{
    public class DetailFundContentDto
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public string Header { get; set; }
        public string ReasonCreatedFund { get; set; }
        public string IdeaCreadtedFund { get; set; }
        public string Footer { get; set; }
    }
}
