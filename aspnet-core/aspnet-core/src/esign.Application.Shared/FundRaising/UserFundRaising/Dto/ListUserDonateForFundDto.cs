using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.UserFundRaising.Dto
{
    public class ListUserDonateForFundDto
    {
        public DateTime? CreatedDonate { get; set; }
        public string UserNameDonate { get; set; }
        public float AmountOfMoney { get; set; }
    }
}
