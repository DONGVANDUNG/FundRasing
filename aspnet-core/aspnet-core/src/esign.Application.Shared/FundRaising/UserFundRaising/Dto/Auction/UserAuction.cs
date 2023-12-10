using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto.Auction
{
    public class UserAuction
    {
        public long? AuctionItemId { get; set; }
        public float? AmountAuction { get; set; }
        public bool? IsPublic { get; set; }
    }
}
