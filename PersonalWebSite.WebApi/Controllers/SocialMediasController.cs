using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediasController : ControllerBase
    {
        private readonly ISocialMediaDal _socialMediaDal;
        public SocialMediasController(ISocialMediaDal socialMediaDal)
        {
            _socialMediaDal = socialMediaDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBanners()
        {
            var values = await _socialMediaDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBannerById(int id)
        {
            var value = await _socialMediaDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(SocialMedia socialMedia)
        {
            await _socialMediaDal.CreateAsync(socialMedia);
            return Ok("Banner information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBanner(SocialMedia socialMedia)
        {
            await _socialMediaDal.UpdateAsync(socialMedia);
            return Ok("Banner information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBanner(int id)
        {
            var value = await _socialMediaDal.GetByIdAsync(id);
            await _socialMediaDal.RemoveAsync(value);
            return Ok("Banner information has been removed.");
        }
    }
}
