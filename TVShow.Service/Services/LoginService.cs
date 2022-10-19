using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShow.Domain.Core;
using TVShow.Domain.ViewModel;
using TVShow.Service.Interfaces.Service;

namespace TVShow.Service.Services
{
    public class LoginService : ILoginService
    {
        private SignInManager<IdentityUser<Guid>> _signManager;

        public LoginService(SignInManager<IdentityUser<Guid>> signManager)
        {
            _signManager = signManager;
        }

        public async Task<string> UserLogin(LoginRequest loginRequest)
        {
            if (_signManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, false, false).Result.Succeeded)
            {
                var identityUser = _signManager.UserManager
                                               .Users
                                               .FirstOrDefault(u => u.NormalizedUserName == loginRequest.UserName.ToUpper());
                return Authentication.GenereteToken(identityUser).Value;
            }
            return null;
        }

        public async Task<bool> LogoutUser()
        {
            return _signManager.SignOutAsync().IsCompleted;
        }
    }
}
