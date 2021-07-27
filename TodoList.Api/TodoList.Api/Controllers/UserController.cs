using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Application.Models;
using Microsoft.AspNetCore.Authorization;
using TodoList.Application.Services.Interfaces;

namespace TodoList.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.Authenticate(model);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRegisterModel userRegisterModel)
        {
            if (userRegisterModel is null)
            {
                return BadRequest();
            }

            var response = await _userService.CreateUserAsync(userRegisterModel);
            return Ok(response);
        }
    }
}
