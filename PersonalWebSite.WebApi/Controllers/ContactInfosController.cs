using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ContactInfoViewModels;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfosController : ControllerBase
    {
        private readonly IContactInfoDal _contactInfoDal;
        private readonly IManagementDal _managementDal;
        public ContactInfosController(IContactInfoDal contactInfoDal, IManagementDal managementDal)
        {
            _contactInfoDal = contactInfoDal;
            _managementDal = managementDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContactInfos()
        {
            var values = await _contactInfoDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactInfoById(int id)
        {
            var value = await _contactInfoDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetContactInfosByUserId/{userId}")]
        public async Task<IActionResult> GetContactInfosByUserId(string userId)
        {
            var values = await _contactInfoDal.GetContactInfosByUserId(userId);
            return Ok(values);
        }

        [HttpGet("GetContactInfosByUserName/{userName}")]
        public async Task<IActionResult> GetContactInfosByUserName(string userName)
        {
            var user = await _managementDal.FindByNameAsync(userName);

            var values = await _contactInfoDal.GetContactInfosByUserId(user.Id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContactInfo(CreateContactInfoViewModel model)
        {
            var contactInfo = new ContactInfo
            {
                Description = model.Description,
                IconUrl = model.IconUrl,
                Title = model.Title,
                UserId = model.UserId
            };
            await _contactInfoDal.CreateAsync(contactInfo);
            return Ok("Contact Info information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContactInfo(UpdateContactInfoViewModel model)
        {
            var contactInfo = new ContactInfo
            {
                ContactInfoId = model.ContactInfoId,
                Description = model.Description,
                IconUrl = model.IconUrl,
                Title = model.Title,
            };
            await _contactInfoDal.UpdateAsync(contactInfo);
            return Ok("Contact Info information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveContactInfo(int id)
        {
            var value = await _contactInfoDal.GetByIdAsync(id);
            await _contactInfoDal.RemoveAsync(value);
            return Ok("Contact Info information has been removed.");
        }
    }
}
