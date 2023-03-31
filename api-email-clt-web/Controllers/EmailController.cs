
using api_email_clt_web.Interfaces;
using api_email_clt_web.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace api_email_clt_web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _services;

        public EmailController(IEmailService services)
        {
            _services = services;
        }


        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail(string fullname, IFormFile file)
        {
            var result = await _services.SendEmail(fullname, file);
            return result;
        }

        [HttpPost("send-contact-message-email")]
        public IActionResult SendContactMessage(ContactMessage message)
        {
            var result = _services.SendContactMessage(message);
            return result;
        }
    }
}
