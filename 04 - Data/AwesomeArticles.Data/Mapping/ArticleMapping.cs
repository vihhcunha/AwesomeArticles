using AwesomeArticles.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Data.Mapping
{
    public class ArticleMapping : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.IdArticle);

            builder.HasMany(x => x.Likes)
                .WithOne(x => x.Article)
                .HasForeignKey(x => x.IdArticle)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
