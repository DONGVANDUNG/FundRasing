﻿using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising
{
    public class GetListFundPackageDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public float? PaymentFee { get; set; }
        public string CreatedTime { get; set; }
        public float? Commission { get; set; }
    }
}
