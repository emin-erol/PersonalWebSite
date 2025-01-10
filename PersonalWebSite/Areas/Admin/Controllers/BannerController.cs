using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.BannerDtos;
using System.Text;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Banner")]
    [Authorize]
    public class BannerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BannerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Banners/GetBannerWithSocialMedia");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<BannerWithSocialMediaDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateBannerModal")]
        public IActionResult CreateBannerModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateBannerModal")]
        public async Task<IActionResult> CreateBanner(CreateBannerDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7007/api/Banners/", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Banner");
            }

            return View("CreateBannerModal", dto);
        }

        [Route("RemoveBanner/{id}")]
        public async Task<IActionResult> RemoveBanner(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("https://localhost:7007/api/Banners?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Banner");
            }

            return View();
        }

        [HttpGet]
        [Route("UpdateBannerModal/{id}")]
        public async Task<IActionResult> UpdateBannerModal(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Banners/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateBannerDto>(jsonData);

                return View(value);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateBannerModal/{id}")]
        public async Task<IActionResult> UpdateBanner(UpdateBannerDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7007/api/Banners/", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Banner");
            }

            return View("UpdateBannerModal", dto);
        }
    }
}
