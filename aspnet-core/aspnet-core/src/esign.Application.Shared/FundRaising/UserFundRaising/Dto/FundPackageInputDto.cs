using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto
{
    public class FundPackageInputDto : PagedAndSortedResultRequestDto
    {
        //public DateTime? CreatedDate { get; set; }
        public string TypePackage { get; set; }
    }
}
