using BookstoreApi.Application.IRepositories;
using BookstoreApi.Domain.Entities;

namespace BookstoreApi.Infrastructure.Repositories
{

    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Author author, CancellationToken cancellationToken)
        {
            await _context.Authors.AddAsync(author, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
