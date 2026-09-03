using BookstoreApi.Domain.Entities;

namespace BookstoreApi.Application.IRepositories
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author author, CancellationToken cancellationToken);
    }
}
