using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVShow.Domain.ViewModel;

namespace TVShow.Service.Interfaces.Service
{
    public interface ILoginService
    {
        Task<string> UserLogin(LoginRequest loginRequest);
        Task<bool> LogoutUser();
    }
}
