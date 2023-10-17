using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiser.Dto
{
    public class UserWarningForDto
    {
        public int Id { get; set; }
        public DateTime? CreatedWarning { get; set; }
        public string Content { get; set; }
        public string LevelWarning { get; set; }
    }
}
