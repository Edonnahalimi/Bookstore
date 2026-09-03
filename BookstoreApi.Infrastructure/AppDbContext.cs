using BookstoreApi.Application.IRepositories;
using BookstoreApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Infrastructure
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
