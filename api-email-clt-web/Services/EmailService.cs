using api_email_clt_web.Interfaces;
using api_email_clt_web.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace api_email_clt_web.Services
{
    public class EmailService : IEmailService
    {

        public async Task<IActionResult> SendEmail(string fullname, string emailTo, IFormFile file)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("pg9609631@outlook.com.ar"));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = "Vacancia CLT - " + fullname;
            var builder = new BodyBuilder();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                builder.Attachments.Add(file.FileName, memoryStream.ToArray());
            }

            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("pg9609631@outlook.com.ar", "_._12345678");
            smtp.Send(email);
            smtp.Disconnect(true);

            return new OkResult();
        }
        public IActionResult SendContactMessage(string emailTo, ContactMessage message)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("pg9609631@outlook.com.ar"));
            email.To.Add(MailboxAddress.Parse(emailTo));
            email.Subject = "Contactos - Mensaje - " + message.fullname;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<!DOCTYPE html>\r\n" +
                       "<html lang=\"en\">\r\n" +
                       "<head>\r\n    " +
                       "<meta charset=\"UTF-8\">\r\n    " +
                       "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    " +
                       "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    " +
                       "<title>Document</title>\r\n</head>\r\n" +
                       $"<body>\r\n    <h2>{message.fullname}</h2>\r\n    <p>{message.email}</p>\r\n    <p>{message.phone}</p>\r\n    <p>{message.message}</p>\r\n</body>\r\n</html>"
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("pg9609631@outlook.com.ar", "_._12345678");
            smtp.Send(email);
            smtp.Disconnect(true);

            return new OkResult();
        }
    }
}
