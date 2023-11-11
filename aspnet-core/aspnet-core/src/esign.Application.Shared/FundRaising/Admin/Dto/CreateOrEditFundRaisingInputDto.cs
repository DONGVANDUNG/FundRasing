using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class CreateOrEditFundRaisingInputDto
    {
        public IFormFile file {  get; set; }
        public string FundName { get; set; }
        public string FundTitle { get; set; }
        public decimal? AmountOfMoney { get; set; }
        public DateTime? FundStartDate { get; set; }
        public DateTime? FundEndDate { get; set; }
        public int FundContentId { get; set; }
    }
}
