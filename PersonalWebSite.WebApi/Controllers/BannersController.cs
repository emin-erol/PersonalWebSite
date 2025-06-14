﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.BannerViewModels;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerDal _bannerDal;
        private readonly IManagementDal _managementDal;
        public BannersController(IBannerDal bannerDal, IManagementDal managementDal)
        {
            _bannerDal = bannerDal;
            _managementDal = managementDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBanners()
        {
            var values = await _bannerDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBannerById(int id)
        {
            var value = await _bannerDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetBannerWithSocialMedia")]
        public async Task<IActionResult> GetBannerWithSocialMedia()
        {
            var values = await _bannerDal.GetBannerWithSocialMedia();
            return Ok(values);
        }

        [HttpGet("GetBannerWithSocialMediaByUserId/{userId}")]
        public async Task<IActionResult> GetBannerWithSocialMediaByUserId(string userId)
        {
            var values = await _bannerDal.GetBannerWithSocialMediaByUserId(userId);
            return Ok(values);
        }

        [HttpGet("GetBannerWithSocialMediaByUserName/{userName}")]
        public async Task<IActionResult> GetBannerWithSocialMediaByUserName(string userName)
        {
            var user = await _managementDal.FindByNameAsync(userName);

            var values = await _bannerDal.GetBannerWithSocialMediaByUserId(user.Id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerViewModel model)
        {
            var banner = new Banner
            {
                StartMessage = model.StartMessage,
                MeetMessage = model.MeetMessage,
                Title = model.Title,
                UserId = model.UserId
            };

            await _bannerDal.CreateAsync(banner);
            return Ok("Banner information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBanner(UpdateBannerViewModel model)
        {
            var banner = new Banner
            {
                BannerId = model.BannerId,
                StartMessage = model.StartMessage,
                MeetMessage = model.MeetMessage,
                Title = model.Title,
            };
            await _bannerDal.UpdateAsync(banner);
            return Ok("Banner information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBanner(int id)
        {
            var value = await _bannerDal.GetByIdAsync(id);
            await _bannerDal.RemoveAsync(value);
            return Ok("Banner information has been removed.");
        }
    }
}
