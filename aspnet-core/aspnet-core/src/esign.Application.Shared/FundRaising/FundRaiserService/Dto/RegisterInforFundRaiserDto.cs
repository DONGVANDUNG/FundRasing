using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class RegisterInforFundRaiserDto
    {
        public int Id { get; set; }

        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public long? FundPackage { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }

    }
}
