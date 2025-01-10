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
        public FootersController(IFooterDal footerDal)
        {
            _footerDal = footerDal;
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

		[HttpPost]
        public async Task<IActionResult> CreateFooter(CreateFooterViewModel model)
        {
            var footer = new Footer
            {
                Description = model.Description
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
                Description = model.Description
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
