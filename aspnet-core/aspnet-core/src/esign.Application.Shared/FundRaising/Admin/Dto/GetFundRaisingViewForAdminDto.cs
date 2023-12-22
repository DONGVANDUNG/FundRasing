using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class GetFundRaisingViewForAdminDto
    {
        public long? Id { get; set; }
        public string PostTitle { get; set; }
        public string FundRaiser { get; set; }
        public string FundName { get; set; }
        public decimal? AmountDonatePresent { get; set; }
        public decimal? AmountDonateTarget { get; set; }
        public decimal? PercentAchieved { get; set; }
        public string PostTopic { get; set; }
        public List<string> ListImageUrl { get; set; }
        public string Unit { get; set; }
        public long? FundId { get; set; }
    }
}
