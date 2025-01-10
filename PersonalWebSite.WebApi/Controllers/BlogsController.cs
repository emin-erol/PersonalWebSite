using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.BlogViewModels;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogDal _blogDal;

        public BlogsController(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var values = await _blogDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var value = await _blogDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogViewModel model)
        {
            var blog = new Blog
            {
                Content = model.Content,
                CreatedDate = DateTime.Now,
                Title = model.Title,
                CoverImageUrl = model.CoverImageUrl
            };

            await _blogDal.CreateAsync(blog);
            return Ok("The Blog has been created!");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlog(UpdateBlogViewModel model)
        {
            var blog = new Blog
            {
                BlogId = model.BlogId,
                Content = model.Content,
                CreatedDate = DateTime.Now,
                Title = model.Title,
                CoverImageUrl = model.CoverImageUrl
            };

            await _blogDal.UpdateAsync(blog);
            return Ok("The Blog has been updated!");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBlog(int id)
        {
            var value = await _blogDal.GetByIdAsync(id);

            await _blogDal.RemoveAsync(value);

            return Ok("The Blog has been removed!");
        }

    }
}
