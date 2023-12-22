﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class RegisterFundPackageInputDto :PagedAndSortedResultRequestDto
    {
        public bool? Status { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
