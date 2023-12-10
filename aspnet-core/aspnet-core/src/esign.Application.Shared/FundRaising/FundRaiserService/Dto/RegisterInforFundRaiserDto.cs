using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class RegisterInforFundRaiserDto
    {
        public long Id { get; set; }
        public long? FundPackageId { get; set; }

        public string Orgnization { get; set; }
        public string Address { get; set; }
        public long? FundPackage { get; set; }
        public string Phone { get; set; }
        public string OrgnizationIntro { get; set; }
        public string Email { get; set; }

    }
}
