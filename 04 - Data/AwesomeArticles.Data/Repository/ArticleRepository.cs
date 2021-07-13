using AwesomeArticles.Domain.Entities;
using AwesomeArticles.Domain.Interfaces;
using AwesomeArticles.Domain.Interfaces.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Data.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AwesomeArticlesContext _context;

        public ArticleRepository(AwesomeArticlesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AddArticle(Article article)
        {
            await _context.Articles.AddAsync(article);
        }

        public async Task<Article> GetArticle(Guid idArticle)
        {
            return await _context.Articles
                .Include(x => x.Likes)
                .FirstOrDefaultAsync(x => x.IdArticle == idArticle);
        }

        public async Task<List<Article>> GetArticles()
        {
            return await _context.Articles.AsNoTracking().ToListAsync();
        }

        public async Task AddLike(ArticleLike articleLike)
        {
            await _context.ArticlesLikes.AddAsync(articleLike);
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }

}
