using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ResumeCategoryViewModels;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeCategoriesController : ControllerBase
    {
        private readonly IResumeCategoryDal _resumeCategoryDal;
        public ResumeCategoriesController(IResumeCategoryDal resumeCategoryDal)
        {
            _resumeCategoryDal = resumeCategoryDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResumeCategories()
        {
            var values = await _resumeCategoryDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResumeCategoryById(int id)
        {
            var value = await _resumeCategoryDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetAllResumeContent")]
        public async Task<IActionResult> GetAllResumeContent()
        {
            var values = await _resumeCategoryDal.GetAllResumeContent();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResumeCategory(CreateResumeCategoryViewModel model)
        {
            var resumeCategory = new ResumeCategory
            {
                CategoryName = model.CategoryName
            };
            await _resumeCategoryDal.CreateAsync(resumeCategory);
            return Ok("Resume Category information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateResumeCategory(UpdateResumeCategoryViewModel model)
        {
            var resumeCategory = new ResumeCategory
            {
                CategoryName = model.CategoryName,
                ResumeCategoryId = model.ResumeCategoryId
            };
            await _resumeCategoryDal.UpdateAsync(resumeCategory);
            return Ok("Resume Category information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveResumeCategory(int id)
        {
            var value = await _resumeCategoryDal.GetByIdAsync(id);
            await _resumeCategoryDal.RemoveAsync(value);
            return Ok("Resume Category information has been removed.");
        }
    }
}
