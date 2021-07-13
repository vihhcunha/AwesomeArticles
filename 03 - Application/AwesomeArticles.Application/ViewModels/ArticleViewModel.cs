using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Application.ViewModels
{
    public class ArticleViewModel
    {
        public Guid IdArticle { get;  set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int TotalLikes { get; set; }
        public bool UserLiked { get; set; }
        public List<ArticleLikeViewModel> Likes { get; set; } = new List<ArticleLikeViewModel>();
    }
}
