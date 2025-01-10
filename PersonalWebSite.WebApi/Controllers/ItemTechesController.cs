using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ItemTechViewModels;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTechesController : ControllerBase
    {
        private readonly IItemTechDal _itemTechDal;
        public ItemTechesController(IItemTechDal itemTechDal)
        {
            _itemTechDal = itemTechDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItemTeches()
        {
            var values = await _itemTechDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("GetItemTechIdByResumeCategoryItemIdAndTechIUsedName")]
        public async Task<IActionResult> GetItemTechIdByResumeCategoryItemIdAndTechIUsedName(int resumeCategoryItemId, string name)
        {
            var values = await _itemTechDal.GetItemTechIdByResumeCategoryItemIdAndTechIUsedName(resumeCategoryItemId, name);
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemTechById(int id)
        {
            var value = await _itemTechDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItemTech(CreateItemTechViewModel model)
        {
            var itemTech = new ItemTech
            {
                ResumeCategoryItemId = model.ResumeCategoryItemId,
                TechIUsedId = model.TechIUsedId
            };
            await _itemTechDal.CreateAsync(itemTech);
            return Ok("Item Tech information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItemTech(UpdateItemTechViewModel model)
        {
            var itemTech = new ItemTech
            {
                ItemTechId = model.ItemTechId,
                ResumeCategoryItemId = model.ResumeCategoryItemId,
                TechIUsedId = model.TechIUsedId
            };
            await _itemTechDal.UpdateAsync(itemTech);
            return Ok("Item Tech information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItemTech(int id)
        {
            var value = await _itemTechDal.GetByIdAsync(id);
            await _itemTechDal.RemoveAsync(value);
            return Ok("Item Tech information has been removed.");
        }
    }
}
