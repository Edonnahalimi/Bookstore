using BookstoreApi.Application.DTOs;
using BookstoreApi.Application.IRepositories;
using MediatR;

namespace BookstoreApi.Application.Books.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto?>
    {
        private readonly IBookRepository _repository;

        public GetBookByIdQueryHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<BookDto?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.BookId, cancellationToken);

            if (book is null)
                return null;

            return new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                SubTitle = book.SubTitle,
                Author = new AuthorDto
                {
                    AuthorId = book.AuthorId,
                    Name = book.Author.Name
                }
            };
        }
    }
}
