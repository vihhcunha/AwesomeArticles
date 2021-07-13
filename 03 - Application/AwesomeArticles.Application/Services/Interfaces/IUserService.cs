using AwesomeArticles.Application.ViewModels;
using AwesomeArticles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> Login(UserViewModel userViewModel);
        Task<UserViewModel> AddUser(UserViewModel userViewModel);
        Task<UserViewModel> GetUser(Guid idUser);
    }
}
