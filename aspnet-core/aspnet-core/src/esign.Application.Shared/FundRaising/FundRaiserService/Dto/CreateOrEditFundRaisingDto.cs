using esign.FundRaising;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class CreateOrEditFundRaisingDto
    {
        //public int Id { get; set; }
        //public string FundName { get; set; }
        //public string TitleFund { get; set; }
        //public string ImageUrl { get; set; }
        //public string Created { get; set; }
        //public float AmountOfMoney { get; set; }
        //public DateTime FundEndDate { get; set; }
        //public DetailFundContentDto ContentOfFund { get; set; }

        public long? Id { get; set; }
        public DateTime? FundRaisingDay { get; set; }
        public string FundName { get; set; }
        public DateTime? FundEndDate { get; set; }
        public float? AmountDonationTarget { get; set; }
    }
}
