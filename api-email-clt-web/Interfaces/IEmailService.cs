using api_email_clt_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_email_clt_web.Interfaces
{
    public interface IEmailService
    {
        Task<IActionResult> SendEmail(string fullname, IFormFile file);
        IActionResult SendContactMessage(ContactMessage message);
    }
}
