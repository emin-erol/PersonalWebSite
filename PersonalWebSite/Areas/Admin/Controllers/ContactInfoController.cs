using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.ContactInfoDtos;
using System.Text;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ContactInfo")]
    [Authorize]
    public class ContactInfoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ContactInfoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/ContactInfos");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultContactInfoDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateContactInfoModal")]
        public async Task<IActionResult> CreateContactInfoModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateContactInfoModal")]
        public async Task<IActionResult> CreateContactInfo(CreateContactInfoDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7007/api/ContactInfos/", content);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ContactInfo");
            }

            return View("CreateContactInfoModal", dto);
        }

        [HttpGet]
        [Route("UpdateContactInfoModal/{id}")]
        public async Task<IActionResult> UpdateContactInfoModal(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/ContactInfos/" + id);

            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateContactInfoDto>(jsonData);

                return View(value);
            }

            return View();
        }
        
        [HttpPost]
        [Route("UpdateContactInfoModal/{id}")]
        public async Task<IActionResult> UpdateContactInfo(UpdateContactInfoDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7007/api/ContactInfos/", content);

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ContactInfo");
            }

            return View("UpdateContactInfoModal", dto);
        }

        [Route("RemoveContactInfo/{id}")]
        public async Task<IActionResult> RemoveContactInfo(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("https://localhost:7007/api/ContactInfos?id=" + id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ContactInfo");
            }

            return View();
        }
    }
}
