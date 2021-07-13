using AwesomeArticles.Domain.Entities;
using AwesomeArticles.Domain.Interfaces.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Domain.Interfaces
{
    public interface IUserRepository : IRepository<Article>
    {
        Task AddUser(User user);
        Task<User> GetUser(Guid idUser);
        Task<User> GetUser(string email);
    }
}
