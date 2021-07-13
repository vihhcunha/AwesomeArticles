using AutoMapper;
using AwesomeArticles.Application.Services.Interfaces;
using AwesomeArticles.Application.ViewModels;
using AwesomeArticles.CrossCutting.Security;
using AwesomeArticles.Domain.Entities;
using AwesomeArticles.Domain.Exceptions;
using AwesomeArticles.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<UserViewModel> AddUser(UserViewModel userViewModel)
        {
            if (userViewModel.Password != userViewModel.ConfirmPassword)
                throw new DomainException("Senhas não coincidem!");

            var userInDatabase = await _userRepository.GetUser(userViewModel.Email);

            if (userInDatabase is not null)
                throw new DomainException("Já há um usuário com esse mesmo e-mail!");

            var user = new User(userViewModel.Name, userViewModel.Email, HashSecurity.EncryptString(userViewModel.Password));

            await _userRepository.AddUser(user);
            await _userRepository.UnitOfWork.Commit();

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> GetUser(Guid idUser)
        {
            var user = await _userRepository.GetUser(idUser);

            if (user is null)
                throw new DomainException("Usuário não existe!");

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<LoginResponse> Login(UserViewModel userViewModel)
        {
            var user = await _userRepository.GetUser(userViewModel.Email);

            if (user is null)
                throw new DomainException("Usuário não existe!");

            if (user.Password != HashSecurity.EncryptString(userViewModel.Password))
                throw new DomainException("Senha incorreta!");

            var token = _tokenService.GerarToken(user.IdUser, user.Email, user.Name);

            return new LoginResponse {
                Token = token,
                User = _mapper.Map<UserViewModel>(user)
            } ;
        }
    }
}
