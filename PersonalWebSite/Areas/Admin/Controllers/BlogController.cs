using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.BlogDtos;
using System.Security.Claims;
using System.Text;

namespace PersonalWebSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Blog")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BlogController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Blogs/GetBlogsByUserId/" + userId);

            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBlogDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpGet]
        [Route("CreateBlogModal")]
        public IActionResult CreateBlogModal()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateBlogModal")]
        public async Task<IActionResult> CreateBlog(CreateBlogDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            dto.UserId = userId;

            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7007/api/Blogs/", content);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Blog");
            }

            return View("CreateBlogModal", dto);
        }

        [HttpGet]
        [Route("UpdateBlogModal/{id}")]
        public async Task<IActionResult> UpdateBlogModal(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7007/api/Blogs/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateBlogDto>(jsonData);

                return View(values);
            }

            return View();
        }

        [HttpPost]
        [Route("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog(UpdateBlogDto dto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7007/api/Blogs/", content);
            
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Blog");
            }

            return View("UpdateBlogModal", dto);
        }

        [Route("RemoveBlog/{id}")]
        public async Task<IActionResult> RemoveBlog(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync("https://localhost:7007/api/Blogs?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Blog");
            }

            return View();
        }
    }
}
