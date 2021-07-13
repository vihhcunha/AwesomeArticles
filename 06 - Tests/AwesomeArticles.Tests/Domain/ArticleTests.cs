using AwesomeArticles.Domain.Entities;
using AwesomeArticles.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AwesomeArticles.Tests.Domain
{
    public class ArticleTests
    {
        public Article _article;
        public ArticleTests()
        {
            _article = new Article("Title 01", "Content 01");
        }

        [Fact(DisplayName = "01 - Build - Must build article correctly")]
        [Trait("Category", "Article")]
        public void Article_BuildArticle_MustBuildCorrectly()
        {
            //Arrange & Act
            var title = "Titulo de teste";
            var content = "Esse é um teste de conteúdo para um artigo";
            var article = new Article(title, content);

            //Assert
            Assert.IsType<Article>(article);
            Assert.Equal(title, article.Title);
            Assert.Equal(content, article.Content);
        }

        [Fact(DisplayName = "01 - Build - Must not build article")]
        [Trait("Category", "Article")]
        public void Article_BuildInvalidArticle_MustNotBuild()
        {
            //Arrange & Act & Assert
            Assert.Throws<DomainException>(() => new Article("", "Conteúdo preenchido"));
            Assert.Throws<DomainException>(() => new Article("Titulo preenchido", ""));
        }

        [Fact(DisplayName = "02 - Like - Must add like correctly")]
        [Trait("Category", "Article")]
        public void Article_AddLike_MustAddCorrectly()
        {
            //Arrange
            var idUser = Guid.NewGuid();

            //Act
            _article.AddLike(idUser);

            //Assert
            Assert.Equal(1,_article.Likes.Count);
            Assert.Equal(idUser, _article.Likes.ToList()[0].IdUser);
            Assert.Equal(_article.IdArticle, _article.Likes.ToList()[0].IdArticle);
        }

        [Fact(DisplayName = "02 - Like - Must not add like")]
        [Trait("Category", "Article")]
        public void Article_AddInvalidLike_MustNotAdd()
        {
            //Arrange
            var idUser = Guid.NewGuid();

            //Act
            _article.AddLike(idUser);

            //Assert
            Assert.Throws<DomainException>(() => _article.AddLike(idUser));
        }

        [Fact(DisplayName = "02 - Like - Must remove like correctly")]
        [Trait("Category", "Article")]
        public void Article_RemoveLike_MustRemoveCorrectly()
        {
            //Arrange
            var idUser = Guid.NewGuid();

            //Act
            _article.AddLike(idUser);
            _article.RemoveLike(idUser);

            //Assert
            Assert.Equal(0, _article.Likes.Count);
        }

        [Fact(DisplayName = "02 - Like - Must not remove like")]
        [Trait("Category", "Article")]
        public void Article_AddInvalidUnlike_MustNotRemove()
        {
            //Arrange
            var idUser = Guid.NewGuid();

            //Act & Assert
            Assert.Throws<DomainException>(() => _article.RemoveLike(idUser));
        }
    }
}
