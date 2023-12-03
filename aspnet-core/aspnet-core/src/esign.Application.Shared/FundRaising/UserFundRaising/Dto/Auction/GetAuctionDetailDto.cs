using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto.Auction
{
    public class GetAuctionDetailDto
    {
        public long? Id { get; set; }
        public string ItemName { get; set; }
        public string IntroduceItem { get; set; }
        public float? AmountJumpMin { get; set; }
        public float? AmountJumpMax { get; set; }
        public float? StartingPrice { get; set; }
        public DateTime StartDate { get; set; }
        public List<string> ListImage { get; set; }
        public string UserName { get; set; }
        public bool? IsPublic { get; set; }
        public float? AuctionPresentAmount { get; set; }
        public int? TimeLeft { get; set; }
        public float? NextMinimumBid { get; set; }
        public float? NextMaximumBid { get; set; }
    }
}
