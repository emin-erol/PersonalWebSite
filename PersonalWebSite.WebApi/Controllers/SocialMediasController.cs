using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.SocialMediaViewModels;
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
        public async Task<IActionResult> GetAllSocialMedias()
        {
            var values = await _socialMediaDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocialMediaById(int id)
        {
            var value = await _socialMediaDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetSocialMediasByUserId/{userId}")]
        public async Task<IActionResult> GetSocialMediasByUserId(string userId)
        {
            var values = await _socialMediaDal.GetSocialMediasByUserId(userId);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaViewModel model)
        {
            var socialMedia = new SocialMedia
            {
                Name = model.Name,
                IconUrl = model.IconUrl,
                SocialMediaUrl = model.SocialMediaUrl,
                UserId = model.UserId!
            };

            await _socialMediaDal.CreateAsync(socialMedia);
            return Ok("SocialMedia information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaViewModel model)
        {
            var socialMedia = new SocialMedia
            {
                SocialMediaId = model.SocialMediaId,
                Name = model.Name,
                IconUrl = model.IconUrl,
                SocialMediaUrl = model.SocialMediaUrl,
            };

            await _socialMediaDal.UpdateAsync(socialMedia);
            return Ok("SocialMedia information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveSocialMedia(int id)
        {
            var value = await _socialMediaDal.GetByIdAsync(id);
            await _socialMediaDal.RemoveAsync(value);
            return Ok("SocialMedia information has been removed.");
        }
    }
}
