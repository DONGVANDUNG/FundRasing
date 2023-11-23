using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto
{
    public class InforDetailBankAcountDto
    {
        public long Id { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string AccountName { get; set; }
        public long UserId { get; set; }
        public float? Balance { get; set; }
        public string Unit { get; set; }

    }
}
