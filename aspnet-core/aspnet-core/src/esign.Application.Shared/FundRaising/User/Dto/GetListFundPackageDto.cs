﻿using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising
{
    public class GetListFundPackageDto
    {
        public int Id { get; set; }
        public float Discount { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public float PaymenFee { get; set; }
    }
}
