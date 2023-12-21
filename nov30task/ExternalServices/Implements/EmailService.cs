using nov30task.ExternalServices.Interfaces;
using System.Net;
using System.Net.Mail;

namespace nov30task.ExternalServices.Implements
{
    public class EmailService : IEmailService
    {
        IConfiguration _configuration { get; }

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(string mailTo, string header, string body, bool isHtml = true)
        {
            SmtpClient smtpClient = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]);

            MailAddress from = new(_configuration["Email:Username"], "Pustok Support team, Ry");
            MailAddress to = new(mailTo);

            MailMessage message = new(from, to)
            {
                Subject = header,
                Body = body,
                IsBodyHtml = isHtml
            };

            smtpClient.Send(message);
        }
    }
}
