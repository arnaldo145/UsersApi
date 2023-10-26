using Microsoft.AspNetCore.Mvc;
using UsersApi.Data.Dtos;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService registerService)
        {
            _userService = registerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserDto createUser)
        {
            await _userService.AddUserAsync(createUser);

            return Ok("Usuário cadastrado!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var token = await _userService.LoginAsync(dto);
            return Ok(token);
        }
    }
}
