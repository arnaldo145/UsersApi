using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsersApi.Data.Dtos;
using UsersApi.Models;

namespace UsersApi.Services
{
    public interface IRegisterService 
    {
        Task AddUserAsync(CreateUserDto createUser);
    }

    public class RegisterService : IRegisterService
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;

        public RegisterService(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task AddUserAsync(CreateUserDto createUser) 
        {
            var user = _mapper.Map<User>(createUser);

            var result = await _userManager.CreateAsync(user, createUser.Password);

            if (!result.Succeeded)
                throw new ApplicationException("Falha ao cadastrar usuário!");
        }
    }
}
