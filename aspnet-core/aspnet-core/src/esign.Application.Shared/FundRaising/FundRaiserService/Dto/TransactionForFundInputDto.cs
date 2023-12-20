using Abp.Application.Services.Dto;
using esign.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class TransactionForFundInputDto : PagedAndSortedResultRequestDto
    {
        public int? FundId { get; set; }
        public DateTime? TransactionTime { get; set; }
    }
}
