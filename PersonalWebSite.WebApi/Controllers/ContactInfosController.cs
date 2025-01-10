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
        public ContactInfosController(IContactInfoDal contactInfoDal)
        {
            _contactInfoDal = contactInfoDal;
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

        [HttpPost]
        public async Task<IActionResult> CreateContactInfo(CreateContactInfoViewModel model)
        {
            var contactInfo = new ContactInfo
            {
                Description = model.Description,
                IconUrl = model.IconUrl,
                Title = model.Title
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
                Title = model.Title
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
