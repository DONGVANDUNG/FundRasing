using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto.Auction
{
    public class InformationAuctionDepositDto
    {
        public string AuctionTitle { get; set; }
        public float? MinAmountDepost { get; set; }
        public float? MaxAmountDepost { get; set; }
    }
}
