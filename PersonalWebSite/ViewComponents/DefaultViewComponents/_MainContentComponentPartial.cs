using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.BannerDtos;

namespace PersonalWebSite.ViewComponents.DefaultViewComponents
{
    public class _MainContentComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public _MainContentComponentPartial(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = _httpContextAccessor.HttpContext?.Request.RouteValues["username"]?.ToString();

            if(String.IsNullOrEmpty(userName) )
            {
                return View();
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Banners/GetBannerWithSocialMediaByUserName/" + userName);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<BannerWithSocialMediaDto>>(jsonData);

                return View(values);
            }
            return View();
        }
    }
}
