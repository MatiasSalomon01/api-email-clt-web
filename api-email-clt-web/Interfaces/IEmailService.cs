using api_email_clt_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_email_clt_web.Interfaces
{
    public interface IEmailService
    {
        Task<IActionResult> SendEmail(string fullname, string emailTo, IFormFile file);
        IActionResult SendContactMessage(string emailTo, ContactMessage message);
    }
}
