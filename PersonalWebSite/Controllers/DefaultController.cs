﻿using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using PersonalWebSite.Dto.ContactMailDtos;
using PersonalWebSite.Dto.ManagementDtos;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string? username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://localhost:7007/api/Managements/GetAllUsers/");

                if (response.IsSuccessStatusCode)
                {
                    var users = await response.Content.ReadFromJsonAsync<List<UserDto>>();
                    var defaultUser = users?.FirstOrDefault();

                    if (defaultUser != null)
                    {
                        return RedirectToAction("Index", "Default", new { username = defaultUser.UserName });
                    }
                }

                return NotFound("Kullanıcı bulunamadı.");
            }

            ViewBag.UserName = username;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SendEmail(CreateContactMailDto dto, string username)
        {
            var client = _httpClientFactory.CreateClient();
            var userResponse = await client.GetAsync("https://localhost:7007/api/Managements/FindByName/" + username);

            if (userResponse.IsSuccessStatusCode)
            {
                var userJson = await userResponse.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserDto>(userJson)!;

                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxAddressFrom = new MailboxAddress("Personal Web Site", "erolemin76@gmail.com");
                MailboxAddress mailboxAddressTo = new MailboxAddress("Admin", user.Email);

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
                dto.UserId = user.Id;

                var jsonData = JsonConvert.SerializeObject(dto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7007/api/ContactMails/", stringContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Default", new { username = username });
                }
            }

            return RedirectToAction("Index", "Default", new { username = username });
        }
    }
}
