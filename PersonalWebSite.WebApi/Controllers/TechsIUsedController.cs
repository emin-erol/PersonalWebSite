using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.TechIUsedViewModels;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechsIUsedController : ControllerBase
    {
        private readonly ITechIUsedDal _techIUsedDal;
        public TechsIUsedController(ITechIUsedDal techIUsedDal)
        {
            _techIUsedDal = techIUsedDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTechIUseds()
        {
            var values = await _techIUsedDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTechIUsedById(int id)
        {
            var value = await _techIUsedDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetTechIUsedIdByTechName/{name}")]
        public async Task<IActionResult> GetAllTechIUseds(string name)
        {
            var values = await _techIUsedDal.GetTechIUsedIdByTechName(name);
            return Ok(values);
        }

        [HttpGet("GetTechIUsedsByUserId/{userId}")]
        public async Task<IActionResult> GetTechIUsedsByUserId(string userId)
        {
            var values = await _techIUsedDal.GetTechIUsedsByUserId(userId);

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTechIUsed(CreateTechIUsedViewModel model)
        {
            var techIUsed = new TechIUsed
            {
                Name = model.Name,
                UserId = model.UserId
            };
            await _techIUsedDal.CreateAsync(techIUsed);
            return Ok("TechIUsed information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTechIUsed(UpdateTechIUsedViewModel model)
        {
            var techIUsed = new TechIUsed
            {
                TechIUsedId = model.TechIUsedId,
                Name = model.Name,
            };
            await _techIUsedDal.UpdateAsync(techIUsed);
            return Ok("TechIUsed information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveTechIUsed(int id)
        {
            var value = await _techIUsedDal.GetByIdAsync(id);
            await _techIUsedDal.RemoveAsync(value);
            return Ok("TechIUsed information has been removed.");
        }
    }
}
