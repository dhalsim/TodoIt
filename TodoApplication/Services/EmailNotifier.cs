using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TodoApplication.Services
{
    public class EmailNotifier : INotifierService
    {
        public async Task SendMessage(string @from, string to, string subject, string message, DateTime dateTime)
        {
            //TODO: some schedular logic to send the message in that time
            //doesn't belong to a web application

            using (var smtp = new SmtpClient("localhost"))
            {
                var mail = new MailMessage
                {
                    Subject = subject,
                    From = new MailAddress(from),
                    Body = message
                };
                mail.To.Add(to);

                await smtp.SendMailAsync(mail);
            }
        }
    }
}