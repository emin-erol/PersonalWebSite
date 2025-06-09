using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.ContactMailDtos;
using System.Security.Claims;
using System.Text;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ContactMail")]
    [Authorize]
    public class ContactMailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ContactMailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/ContactMails/GetContactMailsByUserId/" + userId);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactMailDto>>(jsonData)!;

                values = values.OrderByDescending(mail => mail.ShippingDate).ToList();

                return View(values);
            }

            return View();
        }

        [Route("GetNumberOfUnreadMails")]
        public async Task<int> GetNumberOfUnreadMails()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/ContactMails/GetNumberOfUnreadMails/" + userId);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<int>(jsonData);

                return value;
            }

            return 0;
        }

        [Route("MarkMailAsRead")]
        [HttpPost("MarkMailAsRead/{id}")]
        public async Task<IActionResult> MarkMailAsRead(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(new { id });

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7007/api/ContactMails/MarkMailAsRead?id=" + id, content);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "Mail okundu olarak işaretlenemedi.");
            }
        }

        [Route("RemoveBulk")]
        [HttpDelete("RemoveBulk")]
        public async Task<IActionResult> RemoveBulk([FromBody] List<int> contactMailIds)
        {
            var client = _httpClientFactory.CreateClient();

            string apiUrl = "https://localhost:7007/api/ContactMails/RemoveBulk";
            var jsonData = JsonConvert.SerializeObject(contactMailIds);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Delete, apiUrl)
            {
                Content = content
            };

            try
            {
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Seçilen mailler başarıyla silindi." });
                }
                else
                {
                    return Json(new { success = false, message = "Mail silme işlemi başarısız oldu." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }
    }
}
