using BookstoreApi.Application.Books.Queries;
using BookstoreApi.Domain.Entities;

namespace BookstoreApi.Application.IRepositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> AuthorExistsAsync(int authorId, CancellationToken cancellationToken);
        Task AddAsync(Book book, CancellationToken cancellationToken);
        Task<Book?> GetByIdAsync(int bookId, CancellationToken cancellationToken);
        Task<(List<Book> Books, int TotalCount)> SearchAsync(SearchBooksQuery request, CancellationToken cancellationToken);
        Task UpdateAsync(Book book, CancellationToken cancellationToken);
        Task DeleteAsync(Book book, CancellationToken cancellationToken);
    }
}
