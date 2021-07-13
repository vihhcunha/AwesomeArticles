using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.CrossCutting.Security
{
    public interface ITokenService
    {
        Token GerarToken(Guid idUser, string email, string name);
    }
}
