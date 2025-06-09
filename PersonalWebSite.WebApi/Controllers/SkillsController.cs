using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.SkillViewModels;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillDal _skillDal;
        public SkillsController(ISkillDal skillDal)
        {
            _skillDal = skillDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSkills()
        {
            var values = await _skillDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkillById(int id)
        {
            var value = await _skillDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkill(CreateSkillViewModel model)
        {
            var skill = new Skill
            {
                AboutId = model.AboutId,
                SkillName = model.SkillName,
            };
            await _skillDal.CreateAsync(skill);
            return Ok("Skill information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSkill(UpdateSkillViewModel model)
        {
            var skill = new Skill
            {
                SkillId = model.SkillId,
                AboutId = model.AboutId,
                SkillName = model.SkillName,
            };
            await _skillDal.UpdateAsync(skill);
            return Ok("Skill information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveSkill(int id)
        {
            var value = await _skillDal.GetByIdAsync(id);
            await _skillDal.RemoveAsync(value);
            return Ok("Skill information has been removed.");
        }
    }
}
