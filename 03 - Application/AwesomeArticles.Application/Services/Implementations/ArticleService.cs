using AutoMapper;
using AwesomeArticles.Application.Services.Interfaces;
using AwesomeArticles.Application.ViewModels;
using AwesomeArticles.CrossCutting.User;
using AwesomeArticles.Domain.Entities;
using AwesomeArticles.Domain.Exceptions;
using AwesomeArticles.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Application.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly IAuthenticatedUser _authenticatedUser;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper, IAuthenticatedUser authenticatedUser)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<ArticleViewModel> AddArticle(ArticleViewModel articleViewModel)
        {
            var article = new Article(articleViewModel.Title, articleViewModel.Content);

            await _articleRepository.AddArticle(article);
            await _articleRepository.UnitOfWork.Commit();

            return BuildViewModel(article);
        }

        public async Task<ArticleViewModel> GetArticle(Guid idArticle)
        {
            var article = await _articleRepository.GetArticle(idArticle);

            if (article is null)
                throw new DomainException("Esse artigo não existe!");

            return BuildViewModel(article);
        }

        public async Task<List<ArticleViewModel>> GetArticles()
        {
            var articles = await _articleRepository.GetArticles();

            return BuildViewModel(articles);
        }

        public async Task<ArticleViewModel> AddLike(Guid idArticle)
        {
            var article = await _articleRepository.GetArticle(idArticle);

            if (article is null)
                throw new DomainException("Esse artigo não existe!");

            var articleLike = new ArticleLike(article.IdArticle, _authenticatedUser.IdUser);
            article.AddLike(articleLike);

            await _articleRepository.AddLike(articleLike);
            await _articleRepository.UnitOfWork.Commit();

            return BuildViewModel(article);
        }

        public async Task<ArticleViewModel> RemoveLike(Guid idArticle)
        {
            var article = await _articleRepository.GetArticle(idArticle);

            if (article is null)
                throw new DomainException("Esse artigo não existe!");

            article.RemoveLike(_authenticatedUser.IdUser);
            await _articleRepository.UnitOfWork.Commit();

            return BuildViewModel(article);
        }

        private ArticleViewModel BuildViewModel(Article article)
        {
            var articleViewModel = _mapper.Map<ArticleViewModel>(article);
            articleViewModel.TotalLikes = article.Likes.Count;
            articleViewModel.UserLiked = article.UserLikes(_authenticatedUser.IdUser);

            return articleViewModel;
        }

        private List<ArticleViewModel> BuildViewModel(List<Article> articles)
        {
            var listArticlesViewModel = new List<ArticleViewModel>();

            foreach (var article in articles)
            {
                listArticlesViewModel.Add(BuildViewModel(article));
            }

            return listArticlesViewModel;
        }
    }
}
