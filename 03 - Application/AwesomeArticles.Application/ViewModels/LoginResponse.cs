using AwesomeArticles.CrossCutting.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Application.ViewModels
{
    public class LoginResponse
    {
        public UserViewModel User { get; set; }
        public Token Token { get; set; }
    }
}
