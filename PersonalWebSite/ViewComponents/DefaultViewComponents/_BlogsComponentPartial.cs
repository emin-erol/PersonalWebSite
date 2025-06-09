using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.BlogDtos;

namespace PersonalWebSite.ViewComponents.DefaultViewComponents
{
    public class _BlogsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public _BlogsComponentPartial(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
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
            var response = await client.GetAsync("https://localhost:7007/api/Blogs/GetAllBlogsByUserName/" + username);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBlogDto>>(jsonData);

                return View(values);
            }
            return View();
        }
    }
}
