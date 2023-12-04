using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class GetListFundRaisingDto
    {
        public long? Id { get; set; }
        public DateTime? FundRaisingDay { get; set; }
        public string FundName { get; set; }
        public DateTime? FundEndDate { get; set; }
        public float? AmountDonationTarget { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }
    }
}
