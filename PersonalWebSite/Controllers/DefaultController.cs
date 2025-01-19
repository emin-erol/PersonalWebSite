using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using PersonalWebSite.Dto.ContactMailDtos;
using System.Text;

namespace PersonalWebSite.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(CreateContactMailDto dto)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("Personal Web Site", "erolemin76@gmail.com");
            MailboxAddress mailboxAddressTo = new MailboxAddress("Admin", "erolemin@outlook.com.tr");

            mimeMessage.From.Add(mailboxAddressFrom);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
            <html>
                <body>
                    <h3>Contact Form Submission</h3>
                    <p><strong>Name:</strong> {dto.Name}</p>
                    <p><strong>Email:</strong> {dto.Email}</p>
                    <p><strong>Subject:</strong> {dto.Subject}</p>
                    <p><strong>Message:</strong></p>
                    <p>{dto.Message}</p>
                </body>
            </html>";
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = dto.Subject;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("erolemin76@gmail.com", "oemwerljjnnehchv");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);

            var utcTime = DateTime.UtcNow;
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZoneInfo);

            dto.ShippingDate = localTime;

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7007/api/ContactMails/", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }

            return RedirectToAction("Index", "Default");
        }
    }
}
