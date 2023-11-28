using Abp.Application.Services;
using esign.FundRaising.SendEmail.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.SendEmail
{
    public interface ISendEmail : IApplicationService
    {
        void SendEmail(SendEmailInputDto input);
    }
}
