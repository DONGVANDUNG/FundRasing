using esign.FundRaising.FundRaiserService.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto.Auction
{
    public class CreateOrEditAuctionInputDto
    {
        public long? Id { get; set; }
        public List<GetInforFileDto> File { get; set; }
        public string TitleAuction { get; set; }
        public string ItemName { get; set; }
        public string IntroduceItem { get; set; }
        public int? AmountJumpMin { get; set; }
        public int? AmountJumpMax { get; set; }
        public int? StartingPrice { get; set; }
        public int? LimitedPersionJoin { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float? TargetAmountOfMoney { get; set; }
        public bool? IsPublic { get; set; }
    }
}
