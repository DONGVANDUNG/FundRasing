using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising
{
    public class GetListFundOustandingDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string TopicOfFund { get; set; }
        public DateTime? FundRaisingDay { get; set; }
        public string MainTitle { get; set; }

    }
}
