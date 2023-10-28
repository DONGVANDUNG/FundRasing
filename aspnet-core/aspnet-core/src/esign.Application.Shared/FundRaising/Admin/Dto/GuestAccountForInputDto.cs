using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class GuestAccountForInputDto : PagedAndSortedResultRequestDto
    {
        public DateTime? Created { get; set; }
        public bool? Status { get; set; }
    }
}
