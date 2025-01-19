using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ContactMailViewModels;
using PersonalWebSite.Service.Interfaces;
using PersonalWebSite.Service.Repositories;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMailsController : ControllerBase
    {
        private readonly IContactMailDal _contactMailDal;
        public ContactMailsController(IContactMailDal contactMailDal)
        {
            _contactMailDal = contactMailDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContactMails()
        {
            var values = await _contactMailDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactMailById(int id)
        {
            var value = await _contactMailDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetNumberOfUnreadMails")]
        public async Task<IActionResult> GetNumberOfUnreadMails()
        {
            var value = await _contactMailDal.GetNumberOfUnreadMails();
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContactMail(CreateContactMailViewModel model)
        {
            var contactMail = new ContactMail
            {
                Email = model.Email,
                Message = model.Message,
                Name = model.Name,
                Subject = model.Subject,
                IsRead = false,
                ShippingDate = model.ShippingDate,
            };

            await _contactMailDal.CreateAsync(contactMail);
            return Ok("ContactMail information has been created.");
        }

        [HttpPost("MarkMailAsRead")]
        public async Task<IActionResult> MarkMailAsRead(int id)
        {
            try
            {
                await _contactMailDal.MarkAsRead(id);
                return Ok(new { success = true, message = "Mail başarıyla okundu olarak işaretlendi." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }

    }
}
