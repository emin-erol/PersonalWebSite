using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ContactMailViewModels;
using PersonalWebSite.Service.Interfaces;

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

        [HttpGet("GetNumberOfUnreadMails/{userId}")]
        public async Task<IActionResult> GetNumberOfUnreadMails(string userId)
        {
            var value = await _contactMailDal.GetNumberOfUnreadMails(userId);
            return Ok(value);
        }

        [HttpGet("GetContactMailsByUserId/{userId}")]
        public async Task<IActionResult> GetContactMailsByUserId(string userId)
        {
            var values = await _contactMailDal.GetContactMailByUserId(userId);

            return Ok(values);
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
                UserId = model.UserId!
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

        [HttpDelete("RemoveBulk")]
        public async Task<IActionResult> RemoveBulk([FromBody] List<int> contactMailIds)
        {
            if (contactMailIds == null || !contactMailIds.Any())
            {
                return BadRequest("Silinecek mail ID'leri belirtilmedi.");
            }

            try
            {
                await _contactMailDal.RemoveBulk(contactMailIds);

                return Ok(new { message = "Seçilen mailler başarıyla silindi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Bir hata oluştu.", error = ex.Message });
            }
        }

    }
}
