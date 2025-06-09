using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ResumeCategoryItemViewModels;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeCategoryItemsController : ControllerBase
    {
        private readonly IResumeCategoryItemDal _resumeCategoryItemDal;
        public ResumeCategoryItemsController(IResumeCategoryItemDal resumeCategoryItemDal)
        {
            _resumeCategoryItemDal = resumeCategoryItemDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResumeCategoryItems()
        {
            var values = await _resumeCategoryItemDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResumeCategoryItemById(int id)
        {
            var value = await _resumeCategoryItemDal.GetByIdAsync(id);
            return Ok(value);
        }

		[HttpGet("GetResumeCategoryItemsWithTechIUsed")]
		public async Task<IActionResult> GetResumeCategoryItemsWithTechIUsed()
		{
			var values = await _resumeCategoryItemDal.GetResumeCategoryItemsWithTechIUsed();
			return Ok(values);
		}

        [HttpGet("GetResumeCategoryItemWithTechIUsedByResumeCategoryItemId/{id}")]
        public async Task<IActionResult> GetResumeCategoryItemWithTechIUsedByResumeCategoryItemId(int id)
        {
            var values = await _resumeCategoryItemDal.GetResumeCategoryItemWithTechIUsedByResumeCategoryItemId(id);
            return Ok(values);
        }

        [HttpGet("GetResumeCategoryItemsByResumeCategoryId/{id}")]
        public async Task<IActionResult> GetResumeCategoryItemsByResumeCategoryId(int id)
        {
            var values = await _resumeCategoryItemDal.GetResumeCategoryItemsByResumeCategoryId(id);

            return Ok(values);
        }

        [HttpGet("GetResumeCategoryItemsWithTechIUsedByUserId/{userId}")]
        public async Task<IActionResult> GetResumeCategoryItemsWithTechIUsedByUserId(string userId)
        {
            var values = await _resumeCategoryItemDal.GetResumeCategoryItemsWithTechIUsedByUserId(userId);

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResumeCategoryItem(CreateResumeCategoryItemViewModel model)
        {
            var resumeCategoryItem = new ResumeCategoryItem
            {
                Description = model.Description,
                EndDate = model.EndDate,
                Header = model.Header,
                IconUrl = model.IconUrl,
                StartDate = model.StartDate,
                Title = model.Title,
                ResumeCategoryId = model.ResumeCategoryId,
            };
            await _resumeCategoryItemDal.CreateAsync(resumeCategoryItem);

            var createdItem = new ResumeCategoryItemViewModel
            {
                ResumeCategoryItemId = resumeCategoryItem.ResumeCategoryItemId,
                Description = resumeCategoryItem.Description,
                Header = resumeCategoryItem.Header,
                IconUrl = resumeCategoryItem.IconUrl,
                StartDate = resumeCategoryItem.StartDate,
                EndDate = resumeCategoryItem.EndDate,
                Title = resumeCategoryItem.Title,
            };

            return Ok(createdItem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateResumeCategoryItem(UpdateResumeCategoryItemViewModel model)
        {
            var resumeCategoryItem = new ResumeCategoryItem
            {
                ResumeCategoryItemId = model.ResumeCategoryItemId,
                Description = model.Description,
                EndDate = model.EndDate,
                Header = model.Header,
                IconUrl = model.IconUrl,
                StartDate = model.StartDate,
                Title = model.Title,
                ResumeCategoryId = model.ResumeCategoryId,
            };
            await _resumeCategoryItemDal.UpdateAsync(resumeCategoryItem);
            return Ok("Resume Category Item information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveResumeCategoryItem(int id)
        {
            var value = await _resumeCategoryItemDal.GetByIdAsync(id);
            await _resumeCategoryItemDal.RemoveAsync(value);
            return Ok("Resume Category Item information has been removed.");
        }
    }
}
