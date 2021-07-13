using AwesomeArticles.Domain.Entities;
using AwesomeArticles.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AwesomeArticles.Tests.Domain
{
    public class ArticlesLikeTests
    {
        [Fact(DisplayName = "01 - Build - Must build like correctly")]
        [Trait("Category", "Article like")]
        public void ArticleLike_BuildLike_MustBuildCorrectly()
        {
            //Arrange & Act
            var idUser = Guid.NewGuid();
            var idArticle = Guid.NewGuid();
            var articleLike = new ArticleLike(idArticle, idUser);

            //Assert
            Assert.IsType<ArticleLike>(articleLike);
            Assert.Equal(idArticle, articleLike.IdArticle);
            Assert.Equal(idUser, articleLike.IdUser);
        }

        [Fact(DisplayName = "01 - Build - Must not build article like")]
        [Trait("Category", "Article like")]
        public void ArticleLike_BuildInvalidArticleLike_MustNotBuild()
        {
            //Arrange & Act & Assert
            Assert.Throws<DomainException>(() => new ArticleLike(Guid.Empty, Guid.NewGuid()));
            Assert.Throws<DomainException>(() => new ArticleLike(Guid.NewGuid(), Guid.Empty));
        }
    }
}
