using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalWebSite.Dto.ManagementDtos;
using System.Security.Claims;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace PersonalWebSite.Controllers
{
    public class ManagementController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ManagementController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Register(RegisterDto request)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7007/api/Managements/Register", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var message = "Kayıt İşlemi Başarılı!";
                return RedirectToAction("VerifyModal", new { email = request.Email, message = message});
            }

            return View();
        }

        public async Task<IActionResult> Login(LoginDto request)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(request);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7007/api/Managements/Login", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var responseConfirm = await client.GetAsync("https://localhost:7007/api/Managements/CheckEmailConfirmed/" + request.Email);
                if (responseConfirm.IsSuccessStatusCode)
                {
                    var jsonConfirm = await responseConfirm.Content.ReadAsStringAsync();
                    var check = JsonConvert.DeserializeObject<bool>(jsonConfirm);

                    if(check == true)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, request.Email)
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Index", "Banner", new { area = "Admin" });
                    }
                    else
                    {
                        var message = "Email Doğrulama Yapınız!";
                        return RedirectToAction("VerifyModal", new { email = request.Email, message = message });
                    }
                }
            }

            return View();
        }

        public IActionResult VerifyModal(string email, string message)
        {
            ViewBag.Email = email;
            ViewBag.Message = message;
            return View();
        }

        public async Task<IActionResult> Verify(VerifyDto request)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var requestUrl = $"https://localhost:7007/api/Managements/CheckVerificationCode?email={request.Email}&verificationCode={request.VerificationCode}";

                var response = await client.PostAsync(requestUrl, null);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Doğrulama kodu hatalı veya geçersiz.");
                }
            }
            
            return View(request);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Management");
        }

        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }

        public string GenerateVerificationCode()
        {
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();

            return code;
        }

        public async Task SendVerificationEmailAsync(string email, string verificationCode)
        {
            var smtpClient = new SmtpClient("smtp.erolemin76@gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("erolemin76@gmail.com", "ytfcv.98ef"),
                EnableSsl = true,
            };

            var message = new MailMessage
            {
                From = new MailAddress("erolemin76@gmail.com"),
                Subject = "Kayıt Doğrulama Kodu",
                Body = $"Merhaba, \n\nKayıt işleminizi tamamlamak için doğrulama kodunuz: {verificationCode}",
                IsBodyHtml = true,
            };

            message.To.Add(email);

            await smtpClient.SendMailAsync(message);
        }

        public async Task<IActionResult> ChangeVerificationCode([FromBody] string email)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var response = await client.PostAsync("https://localhost:7007/api/Managements/ChangeVerificationCode?email=" + email, null);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(new { success = true, message = "Doğrulama kodu yeniden gönderildi." });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Doğrulama kodu gönderilemedi." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Bir hata oluştu: {ex.Message}" });
            }
        }
    }
}
