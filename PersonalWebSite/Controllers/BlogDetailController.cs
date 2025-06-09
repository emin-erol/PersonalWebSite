using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.BlogDtos;

namespace PersonalWebSite.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BlogDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Blogs/" + id);
            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<ResultBlogDto>(jsonData);

                return View(value);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Blogs/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var blog = JsonConvert.DeserializeObject<ResultBlogDto>(jsonData);
                return Json(blog);
            }
            return NotFound();
        }
    }
}
