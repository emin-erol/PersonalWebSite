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
        private readonly IManagementDal _managementDal;
        public ResumeCategoriesController(IResumeCategoryDal resumeCategoryDal, IManagementDal managementDal)
        {
            _resumeCategoryDal = resumeCategoryDal;
            _managementDal = managementDal;
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

        [HttpGet("GetAllResumeContent/{userName}")]
        public async Task<IActionResult> GetAllResumeContent(string userName)
        {
            var user = await _managementDal.FindByNameAsync(userName);

            var values = await _resumeCategoryDal.GetAllResumeContent(user.Id);
            return Ok(values);
        }

        [HttpGet("GetResumeCategoriesByUserId/{userId}")]
        public async Task<IActionResult> GetResumeCategoriesByUserId(string userId)
        {
            var values = await _resumeCategoryDal.GetResumeCategoriesByUserId(userId);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResumeCategory(CreateResumeCategoryViewModel model)
        {
            var resumeCategory = new ResumeCategory
            {
                CategoryName = model.CategoryName,
                UserId = model.UserId
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
                ResumeCategoryId = model.ResumeCategoryId,
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
