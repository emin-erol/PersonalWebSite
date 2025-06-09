using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.AboutDtos;

namespace PersonalWebSite.ViewComponents.DefaultViewComponents
{
    public class _AboutMeComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public _AboutMeComponentPartial(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var username = _httpContextAccessor.HttpContext?.Request.RouteValues["username"]?.ToString();

            if (string.IsNullOrEmpty(username))
            {
                return View();
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7007/api/Abouts/GetAboutWithSkill/{username}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<AboutWithSkillDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
