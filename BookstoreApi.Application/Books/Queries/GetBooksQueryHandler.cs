using BookstoreApi.Application.DTOs;
using BookstoreApi.Application.IRepositories;
using MediatR;

namespace BookstoreApi.Application.Books.Queries
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
    {
        private readonly IBookRepository _repository;

        public GetBooksQueryHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _repository.GetAllAsync(cancellationToken);

            return books.Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                SubTitle = b.SubTitle,
                Author = new AuthorDto
                {
                    AuthorId = b.Author.AuthorId,
                    Name = b.Author.Name
                }
            }).ToList();
        }
    }
}
