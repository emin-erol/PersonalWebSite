using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.ContactMailDtos;
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
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/ContactMails");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactMailDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        [Route("GetNumberOfUnreadMails")]
        public async Task<int> GetNumberOfUnreadMails()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/ContactMails/GetNumberOfUnreadMails");
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
    }
}
