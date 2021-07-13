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
    public class ArticleLikeMapping : IEntityTypeConfiguration<ArticleLike>
    {
        public void Configure(EntityTypeBuilder<ArticleLike> builder)
        {
            builder.HasKey(x => x.IdArticlesLike);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.IdUser);
        }
    }
}
