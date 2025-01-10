using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.AboutDtos;
using PersonalWebSite.Dto.SkillDtos;
using System.Text;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/About")]
    [Authorize]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Abouts/GetAboutWithSkill");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<AboutWithSkillDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateAboutModal")]
        public IActionResult CreateAboutModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateAboutModal")]
        public async Task<IActionResult> CreateAbout(CreateAboutDto dto, IFormFile cvFile)
        {
            if (cvFile != null && cvFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Path.GetFileName(cvFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await cvFile.CopyToAsync(stream);
                }

                dto.CvLink = Url.Content($"~/uploads/{fileName}");
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7007/api/Abouts/", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About");
            }

            return View("CreateAboutModal", dto);
        }


        [HttpGet]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Abouts/GetAboutWithSkillByAboutId/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateAboutDto>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto dto, IFormFile cvFile)
        {
            // Yeni CV dosyası varsa, işlemi gerçekleştirelim
            if (cvFile != null && cvFile.Length > 0)
            {
                // Dosyanın yükleneceği klasör
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                // Klasör yoksa oluşturuluyor
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Yeni dosya adı
                var fileName = Path.GetFileName(cvFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Yeni dosyayı yükleme
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await cvFile.CopyToAsync(stream);
                }

                // CvLink'i yeni dosya yolu ile güncelle
                dto.CvLink = Url.Content($"~/uploads/{fileName}");
            }

            // Yetenekler güncelleniyor
            var client = _httpClientFactory.CreateClient();

            var existingSkills = await client.GetAsync("https://localhost:7007/api/Skills");
            if (existingSkills.IsSuccessStatusCode)
            {
                var skillsJsonData = await existingSkills.Content.ReadAsStringAsync();
                var existingSkillValues = JsonConvert.DeserializeObject<List<ResultSkillDto>>(skillsJsonData);

                var updatedSkills = dto.Skills.Select(s => s.SkillId).ToList();
                foreach (var existingSkill in existingSkillValues)
                {
                    if (updatedSkills.Contains(existingSkill.SkillId))
                    {
                        continue;
                    }
                    else
                    {
                        var skillToDeleted = await client.DeleteAsync("https://localhost:7007/api/Skills?id=" + existingSkill.SkillId);
                    }
                }
            }

            // Güncellenmiş bilgiyi API'ye gönderiyoruz
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7007/api/Abouts", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About");
            }

            return View("UpdateAbout", dto);
        }

        [Route("RemoveAbout/{id}")]
        public async Task<IActionResult> RemoveAbout(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("https://localhost:7007/api/Abouts?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About");
            }

            return View();
        }
    }
}
