using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto.Auction
{
    public class AuctionInputDto : PagedAndSortedResultRequestDto
    {
        public int? Status { get; set; }
    }
}
