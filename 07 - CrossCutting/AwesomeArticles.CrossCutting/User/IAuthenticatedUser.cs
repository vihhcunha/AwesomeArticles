using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.CrossCutting.User
{
    public interface IAuthenticatedUser
    {
        Guid IdUser { get; }
        string Name { get; }
        string Email { get; }
    }
}
