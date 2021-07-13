using AwesomeArticles.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Application.Services.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleViewModel> AddArticle(ArticleViewModel articleViewModel);
        Task<ArticleViewModel> GetArticle(Guid idArticle);
        Task<List<ArticleViewModel>> GetArticles();
        Task<ArticleViewModel> AddLike(Guid idArticle);
        Task<ArticleViewModel> RemoveLike(Guid idArticle);
    }
}
