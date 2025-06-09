using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.FooterViewModels;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootersController : ControllerBase
    {
        private readonly IFooterDal _footerDal;
        private readonly IManagementDal _managementDal;
        public FootersController(IFooterDal footerDal, IManagementDal managementDal)
        {
            _footerDal = footerDal;
            _managementDal = managementDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFooters()
        {
            var values = await _footerDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFooterById(int id)
        {
            var value = await _footerDal.GetByIdAsync(id);
            return Ok(value);
        }

		[HttpGet("GetFooterWithSocialMedia")]
		public async Task<IActionResult> GetFooterWithSocialMedia()
		{
			var values = await _footerDal.GetFooterWithSocialMedia();
			return Ok(values);
		}

        [HttpGet("GetFooterWithSocialMediaByUserId/{userId}")]
        public async Task<IActionResult> GetFooterWithSocialMediaByUserId(string userId)
        {
            var values = await _footerDal.GetFooterWithSocialMediaByUserId(userId);
            return Ok(values);
        }

        [HttpGet("GetFooterWithSocialMediaByUserName/{userName}")]
        public async Task<IActionResult> GetFooterWithSocialMediaByUserName(string userName)
        {
            var user = await _managementDal.FindByNameAsync(userName);

            var values = await _footerDal.GetFooterWithSocialMediaByUserId(user.Id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFooter(CreateFooterViewModel model)
        {
            var footer = new Footer
            {
                Description = model.Description,
                UserId = model.UserId
            };
            await _footerDal.CreateAsync(footer);
            return Ok("Footer information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFooter(UpdateFooterViewModel model)
        {
            var footer = new Footer
            {
                FooterId = model.FooterId,
                Description = model.Description,
            };
            await _footerDal.UpdateAsync(footer);
            return Ok("Footer information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFooter(int id)
        {
            var value = await _footerDal.GetByIdAsync(id);
            await _footerDal.RemoveAsync(value);
            return Ok("Footer information has been removed.");
        }
    }
}
