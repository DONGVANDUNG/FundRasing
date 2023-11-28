using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.SendEmail.Dto
{
    public class SendEmailInputDto
    {
        public string EmailReceive { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}