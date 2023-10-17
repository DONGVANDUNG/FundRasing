using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.User.Dto
{
    public class ListFundDonatedForDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int AmountOfMoney { get; set; }
        public string TitleFund { get; set; }
    }
}
