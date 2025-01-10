using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.BannerDtos;

namespace PersonalWebSite.ViewComponents.DefaultViewComponents
{
    public class _MainContentComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _MainContentComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Banners/GetBannerWithSocialMedia");
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
