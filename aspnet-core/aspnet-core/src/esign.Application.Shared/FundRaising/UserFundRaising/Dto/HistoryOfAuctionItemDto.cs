using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto
{
    public class HistoryOfAuctionItemDto
    {
        public long? Id { get; set; }
        public float? AmountOfMoney { get; set; }
        public string AuctionDate { get; set; }
        public string UserAuction { get; set; }
        public bool? IsPublic { get; set; }
    }
}
