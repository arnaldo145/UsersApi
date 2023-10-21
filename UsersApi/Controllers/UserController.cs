using Microsoft.AspNetCore.Mvc;
using UsersApi.Data.Dtos;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private IRegisterService _registerService;

        public UserController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserDto createUser)
        {
            await _registerService.AddUserAsync(createUser);

            return Ok("Usuário cadastrado!");
        }
    }
}
