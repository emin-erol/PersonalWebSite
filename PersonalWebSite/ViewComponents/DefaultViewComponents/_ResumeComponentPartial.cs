using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.ResumeCategoryDtos;

namespace PersonalWebSite.ViewComponents.DefaultViewComponents
{
    public class _ResumeComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public _ResumeComponentPartial(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = _httpContextAccessor.HttpContext?.Request.RouteValues["username"]?.ToString();

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/ResumeCategories/GetAllResumeContent/" + userName);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultResumeCategoryDto>>(jsonData);

                return View(values);
            }
            return View();
        }
    }
}
