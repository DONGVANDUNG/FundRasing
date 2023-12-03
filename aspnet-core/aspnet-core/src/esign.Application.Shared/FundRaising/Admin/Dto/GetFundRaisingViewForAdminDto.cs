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
        public string OrganizationName { get; set; }
        public float? AmountDonatePresent { get; set; }
        public float? AmountDonateTarget { get; set; }
        public float? PercentAchieved { get; set; }
        public string PostTopic { get; set; }
        public List<string> ListImageUrl { get; set; }
        public string Unit { get; set; }
        public long? FundId { get; set; }
    }
}
