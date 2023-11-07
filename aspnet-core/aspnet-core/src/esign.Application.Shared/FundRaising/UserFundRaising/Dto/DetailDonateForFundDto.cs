using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto
{
    public class DetailDonateForFundDto
     {
        public SenderBatchHeader sender_batch_header { get; set; }
        public List<Items> items { get; set; }
    }
    public class Items
    {
        public string recipient_type { get; set; }
        public Amount amount { get; set; }
        public string receiver { get; set; }
        public string note { get; set; }

    }
    public class Amount
    {
        public float value { get; set; }
        public string currency { get; set; }
        
    }
}
