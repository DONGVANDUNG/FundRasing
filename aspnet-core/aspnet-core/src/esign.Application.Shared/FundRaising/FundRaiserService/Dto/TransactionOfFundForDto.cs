using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class TransactionOfFundForDto
    {
        public long? Id { get; set; }
        public float Amount { get; set; }
        public string CreatedTime { get; set; }
        public string Content { get; set; }
        public string UserDonate { get; set; }
        public string FundName { get; set; }
        public string Receiver { get; set; }
        public bool? IsPublic { get; set; }

    }
}
