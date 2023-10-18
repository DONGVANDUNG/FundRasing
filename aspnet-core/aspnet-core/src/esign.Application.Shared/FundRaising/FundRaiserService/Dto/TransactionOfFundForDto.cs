using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class TransactionOfFundForDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public string FundName { get; set; }
    }
}
