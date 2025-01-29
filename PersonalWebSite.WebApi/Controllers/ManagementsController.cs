using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ManagementViewModels;
using PersonalWebSite.Service.Interfaces;

namespace PersonalWebSite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementsController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IManagementDal _managementRepository;
        public ManagementsController(SignInManager<AppUser> signInManager, IManagementDal managementRepository)
        {
            _signInManager = signInManager;
            _managementRepository = managementRepository;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _managementRepository.GetAllUsers();
            if (users.Any())
            {
                return Ok(users);
            }

            return BadRequest("Hiçbir kullanıcı bulunamadı.");
        }

        [HttpGet("CheckEmailConfirmed/{email}")]
        public async Task<IActionResult> CheckEmailConfirmed(string email)
        {
            try
            {
                var check = await _managementRepository.CheckEmailConfirmed(email);
                return Ok(check);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Beklenmeyen bir hata oluştu.", details = ex.Message });
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel request)
        {
            if (ModelState.IsValid)
            {
                var existingUserByEmail = await _managementRepository.FindByEmailAsync(request.Email);
                var existingUserByUserName = await _managementRepository.FindByNameAsync(request.UserName);

                if (existingUserByEmail != null && existingUserByUserName != null)
                {
                    return BadRequest(new { code = "11", message = "Girilen Kullanıcı Adı ve Email başka hesap tarafından kullanılmaktadır." });
                }

                if (existingUserByEmail != null)
                {
                    return BadRequest(new { code = "10", message = "Girilen Email başka hesap tarafından kullanılmaktadır." });
                }

                if (existingUserByUserName != null)
                {
                    return BadRequest(new { code = "01", message = "Girilen Kullanıcı Adı başka hesap tarafından kullanılmaktadır." });
                }

                (bool, AppUser) result = await _managementRepository.Register(request);

                if (result.Item1)
                {
                    return Ok("Kayıt işlemi başarılı.");
                }

                return BadRequest(new { code = "99", message = "Kayıt işlemi başarısız, lütfen daha sonra tekrar deneyiniz." });
            }
            return BadRequest(new { code = "98", message = "Geçersiz model verisi gönderildi." });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (!string.IsNullOrEmpty(request.Email))
            {
                var model = await _managementRepository.Login(request);
                if (model.Item1)
                {
                    await _signInManager.SignInAsync(model.Item2, isPersistent: false);
                    return Ok("Giriş işlemi başarılı.");
                }
                else
                {
                    return BadRequest("Giriş işlemi başarısız.");
                }
            }
            return Unauthorized("Email bulunamadı.");
        }

        [HttpPost("CheckVerificationCode")]
        public async Task<IActionResult> CheckVerificationCode(string email, string verificationCode)
        {
            try
            {
                await _managementRepository.CheckVerificationCodeAsync(email, verificationCode);

                return Ok(new { message = "Doğrulama başarılı." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Beklenmeyen bir hata oluştu.", details = ex.Message });
            }
        }

        [HttpPost("ChangeVerificationCode")]
        public async Task<IActionResult> ChangeVerificationCode(string email)
        {
            try
            {
                await _managementRepository.ChangeVerificationCode(email);

                return Ok(new { message = "Doğrulama kodu değiştirildi." });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Beklenmeyen bir hata oluştu.", details = ex.Message });
            }
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Geçersiz istek");
            }

            var result = await _managementRepository.SendPasswordResetEmailAsync(email);

            if (result)
            {
                return Ok("Şifre sıfırlama e-postası başarıyla gönderildi.");
            }

            return BadRequest("Şifre sıfırlama işlemi başarısız oldu.");
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.NewPassword) || string.IsNullOrEmpty(model.NewPasswordCheck))
            {
                return BadRequest("Geçersiz şifre.");
            }

            if (model.NewPassword != model.NewPasswordCheck)
            {
                return BadRequest("Şifreler eşleşmiyor.");
            }

            var user = await _managementRepository.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var result = await _managementRepository.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok("Şifre başarıyla sıfırlandı.");
            }

            return BadRequest("Şifre sıfırlama işlemi başarısız oldu.");
        }
    }
}