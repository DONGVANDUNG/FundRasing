using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiser.Dto
{
    public class TransactionOfFundForDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public string Fund { get; set; }
    }
}
