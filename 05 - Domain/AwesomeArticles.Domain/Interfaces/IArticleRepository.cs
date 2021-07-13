using AwesomeArticles.Domain.Entities;
using AwesomeArticles.Domain.Interfaces.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Domain.Interfaces
{
    public interface IArticleRepository : IRepository<User>
    {
        Task AddArticle(Article article);
        Task<Article> GetArticle(Guid idArticle);
        Task<List<Article>> GetArticles();
        Task AddLike(ArticleLike articleLike);
    }
}
