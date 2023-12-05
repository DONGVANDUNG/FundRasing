using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class CreateOrEditFundPackageDto :EntityDto<long>
    {
        public string Description { get; set; }
        public string Duration { get; set; }
        public float PaymentFee { get; set; }
        public float Commission { get; set; }
        public bool Status { get; set; }
    }
}
