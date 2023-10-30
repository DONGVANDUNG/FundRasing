using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class GetAllFundRaiserForInputDto : PagedAndSortedResultRequestDto
    {
        public DateTime? Created { get; set; }
        public bool? StatusAccount { get; set; }
        public string Email { get; set; }
    }
}
