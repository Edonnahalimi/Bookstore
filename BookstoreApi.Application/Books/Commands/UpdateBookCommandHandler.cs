using BookstoreApi.Application.IRepositories;
using MediatR;

namespace BookstoreApi.Application.Books.Commands
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IBookRepository _repository;

        public UpdateBookCommandHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.BookId, cancellationToken);

            if (book is null)
                return false;

            var authorExists = await _repository.AuthorExistsAsync(request.AuthorId, cancellationToken);

            if (!authorExists)
                throw new KeyNotFoundException("Author does not exist.");

            book.AuthorId = request.AuthorId;
            book.Title = request.Title;
            book.SubTitle = request.SubTitle;

            await _repository.UpdateAsync(book, cancellationToken);

            return true;
        }
    }
}