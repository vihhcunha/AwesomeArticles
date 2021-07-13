using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Application.ViewModels
{
    public class ArticleLikeViewModel
    {
        public Guid IdArticlesLike { get; set; }
        public Guid IdArticle { get; set; }
        public Guid IdUser { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ArticleViewModel Article { get; set; }
        public UserViewModel User { get; set; }
    }
}
