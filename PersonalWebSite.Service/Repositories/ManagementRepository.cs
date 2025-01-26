using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalWebSite.DAL.Core;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ManagementViewModels;
using PersonalWebSite.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Repositories
{
    public class ManagementRepository : IManagementDal
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly PersonalWebSiteDbContext _context;

        private readonly SmtpClient _smtpClient;
        private readonly string _fromAddress = "noreply@personalwebsite.com";
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "erolemin76@gmail.com";
        private readonly string _smtpPass = "oemwerljjnnehchv";
        public ManagementRepository(UserManager<AppUser> userManager, PersonalWebSiteDbContext context)
        {
            _userManager = userManager;
            _context = context;

            _smtpClient = new SmtpClient(_smtpHost)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true
            };
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<List<AppUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<(bool, AppUser)> Login(LoginViewModel model)
        {
            var user = await this.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var check = await this.CheckPasswordAsync(user, model.Password);
                if (check)
                {
                    return (true, user);
                }
                else
                {
                    return (false, user);
                }
            }
            return (false, user!);
        }

        public async Task<(bool, AppUser)> Register(RegisterViewModel model)
        {
            var verificationCode = GenerateVerificationCode();
            var userModel = new AppUser { UserName = model.UserName, Email = model.Email, VerificationCode = verificationCode};
            var result = await _userManager.CreateAsync(userModel, model.Password);

            if (result.Succeeded)
            {
                await SendVerificationMail(userModel.Email, verificationCode);
                return (true, userModel);
            }
            else
            {
                return (false, userModel);
            }
        }

        public string GenerateVerificationCode()
        {
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();

            return code;
        }

        public async Task CheckVerificationCodeAsync(string email, string verificationCode)
        {
            var user = await this.FindByEmailAsync(email);

            if(user != null)
            {
                if(user.VerificationCode == verificationCode)
                {
                    user.EmailConfirmed = true;
                    await _context.SaveChangesAsync();
                } 
                else
                {
                    throw new InvalidOperationException("Doğrulama kodu hatalı.");
                }
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı bulunamadı.");
            }

        }

        public async Task SendVerificationMail(string email, string verificationCode)
        {
            var subject = "Doğrulama Kodu";
            var body = $@"
            <html>
                <body>
                    <h3>Merhaba,</h3>
                    <p>Kayıt işleminiz başarıyla tamamlandı. İşte doğrulama kodunuz:</p>
                    <h2>{verificationCode}</h2>
                    <p>Bu kodu kullanarak hesabınızı doğrulayabilirsiniz.</p>
                    <br/>
                    <p>Teşekkür ederiz!</p>
                </body>
            </html>";

            var message = new MailMessage
            {
                From = new MailAddress(_fromAddress, "noreply@personalwebsite.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            message.To.Add(email);

            await _smtpClient.SendMailAsync(message);
        }

        public async Task<bool> CheckEmailConfirmed(string email)
        {
            var user = await this.FindByEmailAsync(email);

            if (user != null)
            {
                if (user.EmailConfirmed == true) return true;
                else return false;
            }

            else return false;
        }

        public async Task ChangeVerificationCode(string email)
        {
            var user = await this.FindByEmailAsync(email);
            if (user != null)
            {
                var verificationCode = GenerateVerificationCode();
                user.VerificationCode = verificationCode;

                await _context.SaveChangesAsync();

                await SendVerificationMail(email, verificationCode);
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı bulunamadı.");
            }
        }
    }
}
