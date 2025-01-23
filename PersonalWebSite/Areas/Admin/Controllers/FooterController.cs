using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.FooterDtos;
using System.Text;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Footer")]
    [Authorize]
    public class FooterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public FooterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Footers/GetFooterWithSocialMedia");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<FooterWithSocialMediaDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateFooterModal")]
        public IActionResult CreateFooterModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateFooterModal")]
        public async Task<IActionResult> CreateFooter(CreateFooterDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7007/api/Footers/", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Footer");
            }

            return View("CreateFooterModal", dto);
        }

        [HttpGet]
        [Route("UpdateFooterModal/{id}")]
        public async Task<IActionResult> UpdateFooterModal(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Footers/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateFooterDto>(jsonData);

                return View(value);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateFooterModal/{id}")]
        public async Task<IActionResult> UpdateFooter(UpdateFooterDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7007/api/Footers/", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Footer");
            }

            return View("UpdateFooterModal", dto);
        }
    }
}
