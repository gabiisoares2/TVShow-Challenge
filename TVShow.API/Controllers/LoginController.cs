using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using TVShow.Domain.ViewModel;
using TVShow.Service.Interfaces.ExternalServices;
using TVShow.Service.Interfaces.Service;

namespace TVShow.API.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;
        private IEpisodeDate _episodeDate;
        public LoginController(ILoginService loginService, IEpisodeDate episodeDate)
        {
            _loginService = loginService;
            _episodeDate = episodeDate;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public ActionResult Login(LoginRequest loginRequest)
        {
            var request = _loginService.UserLogin(loginRequest);
            if (!string.IsNullOrEmpty(request.Result))
                return Content(request.Result);
            return Unauthorized();
        }

        [HttpPost("Logout")]
        public ActionResult Logout()
        {
            var response = _loginService.LogoutUser();
            if (response.Result)
                return Ok();
            return NotFound();
        }
    }
}
