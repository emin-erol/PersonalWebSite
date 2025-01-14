﻿using PersonalWebSite.Model.Entities;
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
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<(bool, AppUser)> Login(LoginViewModel model);
        Task<(bool, AppUser)> Register(RegisterViewModel model);
        Task<List<AppUser>> GetAllUsers();
    }
}
