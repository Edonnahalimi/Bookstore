using BookstoreApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Application.IRepositories
{
    public interface IApplicationDbContext
    {
        DbSet<Book> Books { get; }
        DbSet<Author> Authors { get; }
    }
}
