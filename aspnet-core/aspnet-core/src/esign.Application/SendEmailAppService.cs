using esign.FundRaising.SendEmail;
using esign.FundRaising.SendEmail.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace esign
{
    public class SendEmailAppService : ISendEmail
    {
        private static readonly string _from = "vandung030701@gmail.com"; // Email của Sender (của bạn)
        private static readonly string _pass = "bprssyykrrzoiwbp"; //Mật khẩu email
        public SendEmailAppService() { }

        public void SendEmail(SendEmailInputDto input)
        {
            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_from);
                mailMessage.To.Add(input.EmailReceive);
                mailMessage.Subject = input.Subject;
                mailMessage.IsBodyHtml = false;
                mailMessage.Body = input.Body;
                mailMessage.To.Add(input.EmailReceive);
                mailMessage.Priority = MailPriority.High;
                mailMessage.IsBodyHtml = true;

                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(_from, _pass);
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                SmtpServer.Send(mailMessage);
            }

            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi gửi email");
            }
        }
    }
}
