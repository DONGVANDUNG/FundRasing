using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising
{
    public class GetListFundRasingDto
    {
        public long? Id { get; set; }
        public List<string> ListImageUrl { get; set; }
        public string PostTitle { get; set; }
        public decimal? AmountDonatePresent { get; set; }
        public decimal? AmountDonateTarget { get; set; }
        //Phần trăm đạt được
        public decimal? PercentAchieved { get; set; }
        public string OrganizationName { get; set; }
        public string PostTopic { get; set; }

        //Fund Detail
        public string Purpose { get; set; }
        public string Target { get; set; }
        public string Note { get; set; }
        public long? FundId { get; set; }

        // 

    }
}
