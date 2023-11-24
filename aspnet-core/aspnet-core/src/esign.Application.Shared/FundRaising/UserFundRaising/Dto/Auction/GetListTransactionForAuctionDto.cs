using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto.Auction
{
    public class GetListTransactionForAuctionDto
    {
        public long? Id { get; set; }
        public string AuctionerName { get; set; }
        public DateTime? AuctionDate { get; set; }
        public float? AmountAuctionNew { get; set; }
        public float? AmountAuctionOld { get; set; }
        public bool? IsPublic { get; set; }
    }
}
