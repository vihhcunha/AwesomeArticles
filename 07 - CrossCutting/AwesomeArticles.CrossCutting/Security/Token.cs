using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.CrossCutting.Security
{
    public class Token
    {
        public DateTime DataExpiracao { get; set; }
        public string TokenJWT { get; set; }
    }
}
