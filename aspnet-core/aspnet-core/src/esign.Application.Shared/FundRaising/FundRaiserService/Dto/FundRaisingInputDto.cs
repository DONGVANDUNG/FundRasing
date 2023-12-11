using Abp.Application.Services.Dto;
using esign.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto
{
    public class FundRaisingInputDto : PagedAndSortedResultRequestDto
    {
        public int? Status { get; set; }
    }
}
