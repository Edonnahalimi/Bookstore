using BookstoreApi.Application.IRepositories;
using BookstoreApi.Domain.Entities;
using MediatR;

namespace BookstoreApi.Application.Books.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IBookRepository _repository;
        public CreateBookCommandHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var authorExists = await _repository.AuthorExistsAsync(request.AuthorId, cancellationToken);

            if (!authorExists)
            {
                throw new KeyNotFoundException("Author does not exist.");
            }

            var book = new Book
            {
                AuthorId = request.AuthorId,
                Title = request.Title,
                SubTitle = request.SubTitle
            };

            await _repository.AddAsync(book, cancellationToken);

            return book.BookId;
        }
    }
}
