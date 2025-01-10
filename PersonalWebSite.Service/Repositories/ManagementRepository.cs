using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalWebSite.Model.Entities;
using PersonalWebSite.Model.ViewModels.ManagementViewModels;
using PersonalWebSite.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebSite.Service.Repositories
{
    public class ManagementRepository : IManagementDal
    {
        private readonly UserManager<AppUser> _userManager;
        public ManagementRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
            var userModel = new AppUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(userModel, model.Password);

            if (result.Succeeded)
            {
                return (true, userModel);
            }
            else
            {
                return (false, userModel);
            }
        }
    }
}
