using AutoMapper;
using AwesomeArticles.Application.ViewModels;
using AwesomeArticles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Application.Mapper
{
    public class DomainToViewModelMapping : Profile
    {
        public DomainToViewModelMapping()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Article, ArticleViewModel>();
            CreateMap<ArticleLike, ArticleLikeViewModel>();
        }
    }
}
