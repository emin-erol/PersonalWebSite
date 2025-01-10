using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.AboutViewModels;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IAboutDal _aboutDal;
        private readonly ISkillDal _skillDal;
        public AboutsController(IAboutDal aboutDal, ISkillDal skillDal)
        {
            _aboutDal = aboutDal;
            _skillDal = skillDal;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAbouts()
        {
            var values = await _aboutDal.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(int id)
        {
            var value = await _aboutDal.GetByIdAsync(id);
            return Ok(value);
        }

        [HttpGet("GetAboutWithSkill")]
        public async Task<IActionResult> GetAboutWithSkill()
        {
            var values = await _aboutDal.GetAboutWithSkill();
            return Ok(values);
        }

        [HttpGet("GetAboutWithSkillByAboutId/{id}")]
        public async Task<IActionResult> GetAboutWithSkillByAboutId(int id)
        {
            var values = await _aboutDal.GetAboutWithSkillByAboutId(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutViewModel model)
        {
            var about = new About
            {
                StartMessage = model.StartMessage,
                ProfileMessage = model.ProfileMessage,
                BirthDate = model.BirthDate,
                Email = model.Email,
                CvLink = model.CvLink,
                Title = model.Title,
            };
            await _aboutDal.CreateAsync(about);

            foreach (var skillModel in model.Skills)
            {
                var skill = new Skill
                {
                    SkillName = skillModel.SkillName,
                    AboutId = about.AboutId
                };

                await _skillDal.CreateAsync(skill);
            }

            return Ok("About information has been created.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutViewModel model)
        {
            var about = new About
            {
                AboutId = model.AboutId,
                Title = model.Title,
                BirthDate = model.BirthDate,
                Email = model.Email,
                CvLink = model.CvLink,
                ProfileMessage = model.ProfileMessage,
                StartMessage = model.StartMessage
            };
            await _aboutDal.UpdateAsync(about);

            foreach (var skillModel in model.Skills)
            {
                var skill = new Skill
                {
                    SkillId = skillModel.SkillId,
                    SkillName = skillModel.SkillName,
                    AboutId = about.AboutId
                };

                await _skillDal.UpdateAsync(skill);
            }

            return Ok("About information has been updated.");
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAbout(int id)
        {
            var value = await _aboutDal.GetByIdAsync(id);
            await _aboutDal.RemoveAsync(value);
            return Ok("About information has been removed.");
        }
    }
}
