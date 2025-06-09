using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.SocialMediaDtos;
using System.Security.Claims;
using System.Text;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/SocialMedia")]
    [Authorize]
    public class SocialMediaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SocialMediaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/SocialMedias/GetSocialMediasByUserId/" + userId);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSocialMediaDto>>(jsonData);

                return View(values);
            }
            return View();
        }

        [HttpGet]
        [Route("CreateSocialMediaModal")]
        public IActionResult CreateSocialMediaModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateSocialMediaModal")]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            dto.UserId = userId;

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7007/api/SocialMedias/", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "SocialMedia");
            }

            return View("CreateSocialMediaModal", dto);
        }

        [Route("RemoveSocialMedia/{id}")]
        public async Task<IActionResult> RemoveSocialMedia(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("https://localhost:7007/api/SocialMedias?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "SocialMedia");
            }

            return View();
        }

        [HttpGet]
        [Route("UpdateSocialMediaModal/{id}")]
        public async Task<IActionResult> UpdateSocialMediaModal(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/SocialMedias/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateSocialMediaDto>(jsonData);

                return View(value);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateSocialMediaModal/{id}")]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7007/api/SocialMedias/", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "SocialMedia");
            }

            return View("UpdateSocialMediaModal", dto);
        }
    }
}
