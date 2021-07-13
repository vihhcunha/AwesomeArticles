using AwesomeArticles.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Domain.Entities
{
    public class User : Entity
    {

        public Guid IdUser { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime RegistrationDate { get; private set; }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            RegistrationDate = DateTime.Now;
            IdUser = Guid.NewGuid();

            Validate();
        }

        public override void Validate()
        {
            if (String.IsNullOrEmpty(Name))
                throw new DomainException("Defina o nome do usuário!");

            if (String.IsNullOrEmpty(Email))
                throw new DomainException("Defina o e-mail do usuário!");

            if (String.IsNullOrEmpty(Password))
                throw new DomainException("Defina a senha do usuário!");
        }
    }
}
