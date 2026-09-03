using BookstoreApi.Application.IRepositories;
using MediatR;

namespace BookstoreApi.Application.Books.Commands
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBookRepository _repository;

        public DeleteBookCommandHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(request.BookId, cancellationToken);

            if (book is null)
                return false;

            await _repository.DeleteAsync(book, cancellationToken);

            return true;
        }
    }
}
