using AutoMapper;
using System.Threading.Tasks;
using TodoList.Application.Models;
using TodoList.Application.Services.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Enums;
using TodoList.Domain.Repositories;

namespace TodoList.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IUserRepository userRepository, ITokenService tokenService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<BaseModel<UserModel>> Authenticate(LoginModel loginModel)
        {
            var result = await _userRepository.AuthenticateAsync(loginModel.Email, loginModel.Password);

            if (result == default)
            {
                return new BaseModel<UserModel>(false, EMessages.InvalidCredentials);
            }

            var map = _mapper.Map<UserModel>(result);
            map.Token = _tokenService.GenerateToken(map);

            var model = new BaseModel<UserModel>(true, EMessages.Success, map);
            return model;
        }

        public async Task<BaseModel<UserRegisterModel>> CreateUserAsync(UserRegisterModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            await _userRepository.CreateUser(user);
            return new BaseModel<UserRegisterModel>(
                success: true, 
                message: EMessages.Success, 
                data: _mapper.Map<UserRegisterModel>(user)
            );
        }
    }
}
