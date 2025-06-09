using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.FooterDtos;

namespace PersonalWebSite.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
		public _FooterUILayoutComponentPartial(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
		{
			_httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
		}

		public async Task<IViewComponentResult> InvokeAsync()
        {
            var username = _httpContextAccessor.HttpContext?.Request.RouteValues["username"]?.ToString();

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Footers/GetFooterWithSocialMediaByUserName/" + username);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<FooterWithSocialMediaDto>>(jsonData);

                return View(values);
            }
            return View();
        }
    }
}
