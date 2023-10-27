using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class CreateOrEditFundPackageDto :EntityDto<long>
    {
        public int UserId { get; set; }
        public float Discount { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public float PaymenFee { get; set; }
        public bool Status { get; set; }
    }
}
