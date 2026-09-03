using BookstoreApi.Application.Books.Queries;
using BookstoreApi.Application.IRepositories;
using BookstoreApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .OrderBy(b => b.Title)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> AuthorExistsAsync(int authorId, CancellationToken cancellationToken)
        {
            return await _context.Authors
                .AnyAsync(
                    x => x.AuthorId == authorId, cancellationToken);
        }

        public async Task AddAsync(Book book, CancellationToken cancellationToken)
        {
            await _context.Books.AddAsync(book, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(int bookId, CancellationToken cancellationToken)
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.BookId == bookId, cancellationToken);
        }

        public async Task<(List<Book> Books, int TotalCount)> SearchAsync(SearchBooksQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(b =>
                    b.Title.Contains(request.Title));
            }

            if (!string.IsNullOrWhiteSpace(request.Author))
            {
                query = query.Where(b =>
                    b.Author.Name.Contains(request.Author));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var books = await query
                .OrderBy(b => b.Title)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return (books, totalCount);
        }

        public async Task UpdateAsync(Book book, CancellationToken cancellationToken)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Book book, CancellationToken cancellationToken)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
