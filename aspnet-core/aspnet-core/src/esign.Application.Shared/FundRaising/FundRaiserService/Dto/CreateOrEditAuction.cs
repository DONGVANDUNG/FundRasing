using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class CreateOrEditAuction
    {
        public long? Id { get; set; }
        public string TitleAuction { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float? AmountTargetOfMoney { get; set; }
        public bool? IsPublic { get; set; }
        public int?  LimitedOfNumber { get; set; }
    }
}
