using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto.Auction
{
    public class CreateOrEditAuctionInputDto
    {
        public long? Id { get; set; }
        public List<IFormFile> File { get; set; }
        public string TitleAuction { get; set; }
        public string ItemName { get; set; }
        public string IntroduceItem { get; set; }
        public int? AmountJumpMin { get; set; }
        public int? AmountJumpMax { get; set; }
        public int? StartingPrice { get; set; }
        public int? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
