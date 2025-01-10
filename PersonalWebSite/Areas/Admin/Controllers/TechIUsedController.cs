using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.TechIUsedDtos;
using System.Text;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/TechIUsed")]
    [Authorize]
    public class TechIUsedController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TechIUsedController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/TechsIUsed");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultTechIUsedDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateTechIUsedModal")]
        public IActionResult CreateTechIUsedModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateTechIUsedModal")]
        public async Task<IActionResult> CreateTechIUsed(CreateTechIUsedDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7007/api/TechsIUsed/", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "TechIUsed");
            }

            return View("CreateTechIUsedModal", dto);
        }

        [HttpGet]
        [Route("UpdateTechIUsedModal/{id}")]
        public async Task<IActionResult> UpdateTechIUsedModal(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/TechsIUsed/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateTechIUsedDto>(jsonData);

                return View(value);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateTechIUsedModal/{id}")]
        public async Task<IActionResult> UpdateTechIUsed(UpdateTechIUsedDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7007/api/TechsIUsed/", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "TechIUsed");
            }

            return View("UpdateTechIUsedModal", dto);
        }

        [Route("RemoveTechIUsed/{id}")]
        public async Task<IActionResult> RemoveTechIUsed(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("https://localhost:7007/api/TechsIUsed?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "TechIUsed");
            }

            return View();
        }
    }
}
