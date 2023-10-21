using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class GetFundRaisingViewForAdminDto
    {
        public int Id { get; set; }
        public string FundRaiser { get; set; }
        public string FundName { get; set; }
        public decimal AmountOfMoney { get; set; }
        public DateTime? FundRaisingDay { get; set; }
        public DateTime? FundFinishDay { get; set; }
        public string Status { get; set; }
    }
}
