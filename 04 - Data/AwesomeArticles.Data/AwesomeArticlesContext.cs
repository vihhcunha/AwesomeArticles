using AwesomeArticles.Domain.Entities;
using AwesomeArticles.Domain.Interfaces.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeArticles.Data
{
    public class AwesomeArticlesContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleLike> ArticlesLikes { get; set; }

        public AwesomeArticlesContext(DbContextOptions<AwesomeArticlesContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        internal Task FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
