using Abp.Application.Services.Dto;
using esign.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto
{
    public class FundRaisingInputDto
    {
        public string Filter { get; set; }
        public bool? IsPayFee { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
