using AwesomeArticles.Domain.Entities;
using AwesomeArticles.Domain.Interfaces;
using AwesomeArticles.Domain.Interfaces.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AwesomeArticlesContext _context;

        public UserRepository(AwesomeArticlesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetUser(Guid idUser)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.IdUser == idUser);
        }

        public async Task<User> GetUser(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
