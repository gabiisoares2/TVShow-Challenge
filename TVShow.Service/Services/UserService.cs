using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShow.Domain.Entity;
using TVShow.Domain.ViewModel;
using TVShow.Service.Interfaces.Service;

namespace TVShow.Service.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        UserManager<IdentityUser<Guid>> _userManager;

        public UserService(IMapper mapper, UserManager<IdentityUser<Guid>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Guid?> UserRegistration(CreateUserVM createUserVM)
        {
            IdentityUser<Guid> userIdentity = _mapper.Map<IdentityUser<Guid>>(createUserVM);
            var userManager = _userManager.CreateAsync(userIdentity, createUserVM.Password); 
            if (userManager.Result.Succeeded) return userIdentity.Id;
            return null;
        }
    }
}
