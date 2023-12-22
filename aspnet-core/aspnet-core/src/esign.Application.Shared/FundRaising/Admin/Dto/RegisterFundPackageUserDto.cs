using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class RegisterFundPackageUserDto
    {
        public long? Id { get; set; }
        public string UserNameRegister { get; set; }
        public float FundPackage { get; set; }
        public string CreatedRegister { get; set; }
        public string ExpireDate { get; set; }
        public string IsExpired { get; set; }
        public string Status { get; set; }
        public string Duration { get; set; }
    }
}
