using AwesomeArticles.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Domain.Entities
{
    public class ArticleLike : Entity
    {
        public Guid IdArticlesLike { get; private set; }
        public Guid IdArticle { get; private set; }
        public Guid IdUser { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public Article Article { get; private set; }
        public User User { get; private set; }

        public ArticleLike(Guid idArticle, Guid idUser)
        {
            IdArticle = idArticle;
            IdUser = idUser;
            RegistrationDate = DateTime.Now;
            IdArticlesLike = Guid.NewGuid();

            Validate();
        }

        public override void Validate()
        {
            if (IdArticle == Guid.Empty)
                throw new DomainException("Defina o artigo para dar o like!");

            if (IdUser == Guid.Empty)
                throw new DomainException("Defina o usuário para dar o like!");
        }
    }
}
