using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class UserAccountForViewDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserNameLogin { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string LevelWarning { get; set; }
    }
}
