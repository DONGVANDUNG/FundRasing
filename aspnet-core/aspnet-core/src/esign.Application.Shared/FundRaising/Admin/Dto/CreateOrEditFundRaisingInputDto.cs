using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class CreateOrEditFundRaisingInputDto
    {
        public List<IFormFile> File {  get; set; }
        public string FundName { get; set; }
        public string FundTitle { get; set; }
        public float AmountOfMoney { get; set; }
        public DateTime FundStartDate { get; set; }
        public DateTime FundEndDate { get; set; }
        public string FundContent { get; set; }
        public string ReasonCreateFund { get; set; }
        public bool IsPayFee { get; set; }    

    }
}
