using AwesomeArticles.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Domain.Entities
{
    public class Article : Entity
    {
        public Guid IdArticle { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public IReadOnlyCollection<ArticleLike> Likes => _likes;
        private readonly List<ArticleLike> _likes;

        public Article(string title, string content)
        {
            Title = title;
            Content = content;
            RegistrationDate = DateTime.Now;
            IdArticle = Guid.NewGuid();
            _likes = new();

            Validate();
        }

        public override void Validate()
        {
            if (String.IsNullOrEmpty(Title))
                throw new DomainException("Define o título do artigo!");

            if (String.IsNullOrEmpty(Content))
                throw new DomainException("Define o conteúdo do artigo!");
        }

        public void AddLike(Guid idUser)
        {
            ValidateLike(idUser);

            var articleLike = new ArticleLike(IdArticle, idUser);
            _likes.Add(articleLike);
        }

        public void AddLike(ArticleLike articleLike)
        {
            ValidateLike(articleLike.IdUser);
            _likes.Add(articleLike);
        }

        private void ValidateLike(Guid idUser)
        {
            if (UserLikes(idUser))
                throw new DomainException("Não é possível adicionar um like, visto que você já deu um like!");
        }

        public void RemoveLike(Guid idUser)
        {
            if (!UserLikes(idUser))
                throw new DomainException("Não é possível remover o like, visto que você não deu um like!");

            _likes.Remove(_likes.FirstOrDefault(x => x.IdUser == idUser));
        }

        public bool UserLikes(Guid idUser)
        {
            return _likes.Any(x => x.IdUser == idUser);
        }
    }
}
