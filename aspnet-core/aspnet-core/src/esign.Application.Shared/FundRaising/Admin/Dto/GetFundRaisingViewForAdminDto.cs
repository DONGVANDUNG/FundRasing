using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class GetFundRaisingViewForAdminDto
    {
        public int Id { get; set; }
        public string FundRaiser { get; set; }
        public string FundName { get; set; }
        public float AmountOfMoney { get; set; }
        public DateTime? FundRaisingDay { get; set; }
        public DateTime? FundFinishDay { get; set; }
        public string Status { get; set; }
        public string FundTitle { get; set; }
        public DateTime FundStartDate { get; set; }
        public List<string> ListImageUrl { get; set; }
        public string Unit { get; set; }
    }
}
