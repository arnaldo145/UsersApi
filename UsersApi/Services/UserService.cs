using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsersApi.Data.Dtos;
using UsersApi.Models;

namespace UsersApi.Services
{
    public interface IUserService 
    {
        Task AddUserAsync(CreateUserDto createUser);
        Task<string> LoginAsync(LoginUserDto dto);
    }

    public class UserService : IUserService
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private ITokenService _tokenService;

        public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task AddUserAsync(CreateUserDto createUser) 
        {
            var user = _mapper.Map<User>(createUser);

            var result = await _userManager.CreateAsync(user, createUser.Password);

            if (!result.Succeeded)
                throw new ApplicationException("Falha ao cadastrar usuário!");
        }

        public async Task<string> LoginAsync(LoginUserDto dto)
        {
            var callback = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if (!callback.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }

            var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

            var token = _tokenService.GenerateToken(user);

            return token;
        }
    }
}
