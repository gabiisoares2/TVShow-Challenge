using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TVShow.Domain.ViewModel;
using TVShow.Service.Interfaces.Service;

namespace TVShow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IUserService _userService;
        public RegistrationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult RegistrationUser(CreateUserVM vm)
        {
            if (ModelState.IsValid)
            {
                var content = _userService.UserRegistration(vm);
                if(content != null)
                    return Ok(new { Type = HttpStatusCode.Created, Result = content, Message = "User was registered successfully!" });
                return NoContent();
            }
            return Ok(new { Type = HttpStatusCode.NotAcceptable, Message = string.Concat("Please, check this value: {0}", ModelState.Values)});
        }
    }
}
