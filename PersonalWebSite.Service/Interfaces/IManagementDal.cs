using Microsoft.AspNetCore.Identity;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ManagementViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Interfaces
{
    public interface IManagementDal
    {
        Task<AppUser> FindByEmailAsync(string email);
        Task<AppUser> FindByNameAsync(string userName);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<(bool, AppUser)> Login(LoginViewModel model);
        Task<(bool, AppUser)> Register(RegisterViewModel model);
        Task<List<AppUser>> GetAllUsers();
        string GenerateVerificationCode();
        Task CheckVerificationCodeAsync(string email, string verificationCode);
        Task SendVerificationMail(string email, string verificationCode);
        Task<bool> SendPasswordResetEmailAsync(string email);
        Task<bool> CheckEmailConfirmed(string email);
        Task ChangeVerificationCode(string email);
        Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword);
    }
}
