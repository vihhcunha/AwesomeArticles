using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Domain.Entities
{
    public abstract class Entity
    {
        public abstract void Validate();

        public override string ToString()
        {
            return $"{GetType().Name}";
        }
    }
}
