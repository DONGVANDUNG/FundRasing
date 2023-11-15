using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class RegisterInforFundRaiserDto
    {
        public int Id { get; set; }

        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Introduce { get; set; }
        public string Position { get; set; }
        public int Surplus { get; set; }
        public string Unit { get; set; }

    }
}
