using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class InformationWebDto
    {
        public int? Project { get; set; }
        public int? FundRaiser { get; set; }
        public int? AmountDonate { get; set; }
        public float? AmountOfMoneyDonate { get; set; }
    }
}
