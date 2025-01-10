using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.ItemTechDtos;
using PersonalWebSite.Dto.ResumeCategoryDtos;
using PersonalWebSite.Dto.ResumeCategoryItemDtos;
using PersonalWebSite.Dto.TechIUsedDtos;
using System.Text;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ResumeCategoryItem")]
    [Authorize]
    public class ResumeCategoryItemController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ResumeCategoryItemController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/ResumeCategoryItems/GetResumeCategoryItemsWithTechIUsed");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultResumeCategoryItemsWithTechIUsedDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateResumeCategoryItemModal")]
        public async Task<IActionResult> CreateResumeCategoryItemModal()
        {
            var client = _httpClientFactory.CreateClient();
            var responseTechIUsed = await client.GetAsync("https://localhost:7007/api/TechsIUsed");
            var responseResumeCategory = await client.GetAsync("https://localhost:7007/api/ResumeCategories");
            if (responseTechIUsed.IsSuccessStatusCode && responseResumeCategory.IsSuccessStatusCode)
            {
                var techIUsedJson = await responseTechIUsed.Content.ReadAsStringAsync();
                var resumeCategoryJson = await responseResumeCategory.Content.ReadAsStringAsync();

                var techIUsedValues = JsonConvert.DeserializeObject<List<ResultTechIUsedDto>>(techIUsedJson)!;
                var resumeCategoryValues = JsonConvert.DeserializeObject<List<ResultResumeCategoryDto>>(resumeCategoryJson)!;

                Dictionary<int,string> techsIUsed = new Dictionary<int,string>();
                Dictionary<int, string> resumeCategories = new Dictionary<int, string>();


                foreach (var value in techIUsedValues)
                {
                    techsIUsed.Add(value.TechIUsedId, value.Name);
                }

                foreach (var value in resumeCategoryValues)
                {
                    resumeCategories.Add(value.ResumeCategoryId, value.CategoryName);
                }

                ViewBag.ResumeCategories = resumeCategories;
                ViewBag.TechsIUsed = techsIUsed;
            }
            return View();
        }

        [HttpPost]
        [Route("CreateResumeCategoryItemModal")]
        public async Task<IActionResult> CreateResumeCategoryItem(CreateResumeCategoryItemDto dto, string ItemTechesJson)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7007/api/ResumeCategoryItems/", content);

            if (!response.IsSuccessStatusCode)
            {
                return View("CreateResumeCategoryItemModal", dto);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdItem = JsonConvert.DeserializeObject<CreateResumeCategoryItemResponseDto>(responseContent)!;
            var resumeCategoryItemId = createdItem.ResumeCategoryItemId;

            var selectedTeches = JsonConvert.DeserializeObject<List<ResultItemTechDto>>(ItemTechesJson);

            foreach (var tech in selectedTeches)
            {
                var itemTech = new CreateItemTechDto
                {
                    ResumeCategoryItemId = resumeCategoryItemId,
                    TechIUsedId = tech.TechIUsedId
                };

                var itemTechesJson = JsonConvert.SerializeObject(itemTech);
                StringContent itemTechesContent = new StringContent(itemTechesJson, Encoding.UTF8, "application/json");

                var itemTechesResponse = await client.PostAsync("https://localhost:7007/api/ItemTeches/", itemTechesContent);
                if (!itemTechesResponse.IsSuccessStatusCode)
                {
                    return View("CreateResumeCategoryItemModal", dto);
                    
                }
            }

            return RedirectToAction("Index", "ResumeCategoryItem");
        }

        [HttpGet]
        [Route("UpdateResumeCategoryItemModal/{id}")]
        public async Task<IActionResult> UpdateResumeCategoryItemModal(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/ResumeCategoryItems/GetResumeCategoryItemWithTechIUsedByResumeCategoryItemId/" + id);
            var responseResumeCategory = await client.GetAsync("https://localhost:7007/api/ResumeCategories");
            var responseTechIUsed = await client.GetAsync("https://localhost:7007/api/TechsIUsed");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var resumeCategoryJson = await responseResumeCategory.Content.ReadAsStringAsync();
                var techIUsedJson = await responseTechIUsed.Content.ReadAsStringAsync();

                var value = JsonConvert.DeserializeObject<UpdateResumeCategoryItemDto>(jsonData);
                var resumeCategoryValues = JsonConvert.DeserializeObject<List<ResultResumeCategoryDto>>(resumeCategoryJson)!;
                var techIUsedValues = JsonConvert.DeserializeObject<List<ResultTechIUsedDto>>(techIUsedJson)!;

                Dictionary<int, string> resumeCategories = new Dictionary<int, string>();
                Dictionary<int, string> techsIUsed = new Dictionary<int, string>();

                foreach (var resumeCategory in resumeCategoryValues)
                {
                    resumeCategories.Add(resumeCategory.ResumeCategoryId, resumeCategory.CategoryName);
                }

                foreach (var techIUsed in techIUsedValues)
                {
                    techsIUsed.Add(techIUsed.TechIUsedId, techIUsed.Name);
                }

                ViewBag.ResumeCategories = resumeCategories;
                ViewBag.TechsIUsed = techsIUsed;

                return View(value);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateResumeCategoryItemModal/{id}")]
        public async Task<IActionResult> UpdateResumeCategoryItem(UpdateResumeCategoryItemDto dto, string ItemTechesJson)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("https://localhost:7007/api/ResumeCategoryItems/", content);
            if (!response.IsSuccessStatusCode)
            {
                return View("UpdateResumeCategoryItem", dto);
            }

            var responseResumeCategoryItemWithTechIUsed = await client.GetAsync("https://localhost:7007/api/ResumeCategoryItems/GetResumeCategoryItemWithTechIUsedByResumeCategoryItemId/" + dto.ResumeCategoryItemId);
            if (!response.IsSuccessStatusCode)
            {
                return View("UpdateResumeCategoryItem", dto);
            }

            var resumeCategoryItemWithTechIUsedJson = await responseResumeCategoryItemWithTechIUsed.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<ResultResumeCategoryItemsWithTechIUsedDto>(resumeCategoryItemWithTechIUsedJson);

            var itemTeches = JsonConvert.DeserializeObject<List<ResultItemTechDto>>(ItemTechesJson);

            var valuesToCreated = itemTeches.Where(it => !values.TechNames.Contains(it.ItemTechName)).ToList();

            foreach (var value in valuesToCreated)
            {
                var itemTechDto = new CreateItemTechDto
                {
                    ResumeCategoryItemId = dto.ResumeCategoryItemId,
                    TechIUsedId = value.TechIUsedId,
                };

                var itemTechJson = JsonConvert.SerializeObject(itemTechDto);
                StringContent itemTechContent = new StringContent(itemTechJson, Encoding.UTF8, "application/json");

                var responseItemTech = await client.PostAsync("https://localhost:7007/api/ItemTeches/", itemTechContent);
            }

            var valuesToDeleted = values.TechNames.Where(tn => !itemTeches.Any(it => it.ItemTechName == tn)).ToList();

            foreach (var value in valuesToDeleted)
            {
                var responseItemTech = await client.GetAsync($"https://localhost:7007/api/ItemTeches/GetItemTechIdByResumeCategoryItemIdAndTechIUsedName?resumeCategoryItemId={dto.ResumeCategoryItemId}&name={value}");
                if (responseItemTech.IsSuccessStatusCode)
                {
                    var itemTechJson = await responseItemTech.Content.ReadAsStringAsync();
                    var itemTechIdValue = JsonConvert.DeserializeObject<int>(itemTechJson);

                    var toDeletedItemTechResponse = await client.DeleteAsync("https://localhost:7007/api/ItemTeches?id=" + itemTechIdValue);
                    if (!toDeletedItemTechResponse.IsSuccessStatusCode)
                    {
                        return View("UpdateResumeCategoryItem", dto);
                    }
                }
            }

            return RedirectToAction("Index", "ResumeCategoryItem");

        }

        [Route("RemoveResumeCategoryItem/{id}")]
        public async Task<IActionResult> RemoveResumeCategoryItem(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("https://localhost:7007/api/ResumeCategoryItems?id=" + id);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "ResumeCategoryItem");
            }

            return View();
        }
    }
}
