using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.AboutDtos;
using PersonalWebSite.Dto.SkillDtos;
using System.Security.Claims;
using System.Text;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/About")]
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Abouts/GetAboutWithSkillByUserId/" + userId);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<AboutWithSkillDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("GetProfileImageLink")]
        public async Task<string> GetProfileImageLink()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7007/api/Abouts/GetProfileImageLink");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                return jsonData!;
            }

            return String.Empty;
        }

        [HttpGet]
        [Authorize]
        [Route("CreateAboutModal")]
        public IActionResult CreateAboutModal()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [Route("CreateAboutModal")]
        public async Task<IActionResult> CreateAbout(CreateAboutDto dto, IFormFile cvFile, IFormFile ppFile)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            dto.UserId = userId;

            if (cvFile != null && cvFile.Length > 0 && ppFile != null && ppFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var cvFileName = Path.GetFileName(cvFile.FileName);
                var cvFilePath = Path.Combine(uploadsFolder, cvFileName);

                using (var stream = new FileStream(cvFilePath, FileMode.Create))
                {
                    await cvFile.CopyToAsync(stream);
                }

                dto.CvLink = Url.Content($"~/uploads/{cvFileName}");

                var ppFileName = Path.GetFileName(ppFile.FileName);
                var ppFilePath = Path.Combine(uploadsFolder, ppFileName);

                using (var stream = new FileStream(ppFilePath, FileMode.Create))
                {
                    await ppFile.CopyToAsync(stream);
                }

                dto.ProfileImageLink = Url.Content($"~/uploads/{ppFileName}");
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
        [Authorize]
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
        [Authorize]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto dto, IFormFile cvFile, IFormFile ppFile)
        {
            if (cvFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                var cvFileName = Path.GetFileName(cvFile.FileName);
                var cvFilePath = Path.Combine(uploadsFolder, cvFileName);

                using (var stream = new FileStream(cvFilePath, FileMode.Create))
                {
                    await cvFile.CopyToAsync(stream);
                }

                dto.CvLink = Url.Content($"~/uploads/{cvFileName}");
            }

            if(ppFile != null)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                var ppFileName = Path.GetFileName(ppFile.FileName);
                var ppFilePath = Path.Combine(uploadsFolder, ppFileName);

                using (var stream = new FileStream(ppFilePath, FileMode.Create))
                {
                    await ppFile.CopyToAsync(stream);
                }

                dto.ProfileImageLink = Url.Content($"~/uploads/{ppFileName}");
            }

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

            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7007/api/Abouts", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "About");
            }

            return View("UpdateAbout", dto);
        }
    }
}
