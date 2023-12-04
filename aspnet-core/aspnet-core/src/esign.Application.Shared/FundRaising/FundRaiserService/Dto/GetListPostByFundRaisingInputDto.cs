using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class GetListPostByFundRaisingInputDto :PagedAndSortedResultRequestDto
    {
        public long? FundId { get; set; }
    }
}
