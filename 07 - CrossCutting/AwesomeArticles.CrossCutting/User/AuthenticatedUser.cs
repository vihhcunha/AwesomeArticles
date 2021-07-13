using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.CrossCutting.User
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid IdUser { 
            get
            {
                return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("Id").Value);
            } 
        }

        public string Name
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.FindFirst("Name").Value;
            }
        }

        public string Email
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.FindFirst("Email").Value;
            }
        }
    }
}
