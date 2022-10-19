using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShow.Domain.ViewModel;

namespace TVShow.Service.Interfaces.Service
{
    public interface IUserService
    {
        Task<Guid?> UserRegistration(CreateUserVM createUserVM);
    }
}