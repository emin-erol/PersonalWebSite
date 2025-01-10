using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.ResumeCategoryDtos;
using PersonalWebSite.Dto.TechIUsedDtos;
using System.Text;

namespace PersonalWebSite.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/ResumeCategory")]
    [Authorize]
    public class ResumeCategoryController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public ResumeCategoryController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var response = await client.GetAsync("https://localhost:7007/api/ResumeCategories");
			if (response.IsSuccessStatusCode)
			{
				var jsonData = await response.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultResumeCategoryDto>>(jsonData);

				return View(values);
			}

			return View();
		}

		[HttpGet]
		[Route("CreateResumeCategoryModal")]
		public IActionResult CreateResumeCategoryModal()
		{
			return View();
		}

		[HttpPost]
		[Route("CreateResumeCategoryModal")]
		public async Task<IActionResult> CreateResumeCategory(CreateResumeCategoryDto dto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(dto);
			StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var response = await client.PostAsync("https://localhost:7007/api/ResumeCategories/", content);
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "ResumeCategory");
			}

			return View("CreateResumeCategoryModal", dto);
		}

		[HttpGet]
		[Route("UpdateResumeCategoryModal/{id}")]
		public async Task<IActionResult> UpdateResumeCategoryModal(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var response = await client.GetAsync("https://localhost:7007/api/ResumeCategories/" + id);
			if (response.IsSuccessStatusCode)
			{
				var jsonData = await response.Content.ReadAsStringAsync();
				var value = JsonConvert.DeserializeObject<UpdateResumeCategoryDto>(jsonData);

				return View(value);
			}

			return View();
		}

		[HttpPost]
		[Route("UpdateResumeCategoryModal/{id}")]
		public async Task<IActionResult> UpdateResumeCategory(UpdateResumeCategoryDto dto)
		{
			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(dto);
			StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			var response = await client.PutAsync("https://localhost:7007/api/ResumeCategories/", content);
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "ResumeCategory");
			}

			return View("UpdateResumeCategoryModal", dto);
		}

		[Route("RemoveResumeCategory/{id}")]
		public async Task<IActionResult> RemoveResumeCategory(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var response = await client.DeleteAsync("https://localhost:7007/api/ResumeCategories?id=" + id);
			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "ResumeCategory");
			}

			return View();
		}
	}
}
