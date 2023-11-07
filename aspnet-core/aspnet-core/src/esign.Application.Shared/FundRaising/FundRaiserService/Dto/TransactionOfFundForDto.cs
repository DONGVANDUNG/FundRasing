using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class TransactionOfFundForDto
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public string FundName { get; set; }
        public string Receiver { get; set; }
    }
}
