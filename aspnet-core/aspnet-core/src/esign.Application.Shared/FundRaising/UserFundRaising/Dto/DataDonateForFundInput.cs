using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto
{
    public class DataDonateForFundInput
    {
        public long Id { get; set; }
        public string NoteTransaction { get; set; }
        public int AmountOfMoney { get; set; }
        public int FundId { get; set; }
        public bool? IsPublic { get; set; }
    }
}
