using AutoMapper;
using System.Threading.Tasks;
using TodoList.Application.Models;
using TodoList.Application.Services.Interfaces;
using TodoList.Application.Validators;
using TodoList.Domain.Entities;
using TodoList.Domain.Enums;
using TodoList.Domain.Repositories;
using TodoList.Shared;

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
            var result = await _userRepository.AuthenticateAsync(loginModel.Email, Md5HashExtensions.CreateMD5(loginModel.Password));

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
            var validator = await new UserValidator().ValidateAsync(userModel);
            if (!validator.IsValid)
            {
                return new BaseModel<UserRegisterModel>(false, validator.Errors);
            }

            if(_userRepository.GetUserIdByEmail(userModel.Email) > 0)
            {
                return new BaseModel<UserRegisterModel>(false, EMessages.UserAlreadyExists);
            }

            var user = _mapper.Map<User>(userModel);
            user.Password = Md5HashExtensions.CreateMD5(userModel.Password);
            user.Profile = Domain.EProfile.Client;
            await _userRepository.CreateUser(user);
            return new BaseModel<UserRegisterModel>(true, EMessages.Success, _mapper.Map<UserRegisterModel>(user));
        }

        public int GetUserId(string email)
        {
            return _userRepository.GetUserIdByEmail(email);
        }
    }
}
