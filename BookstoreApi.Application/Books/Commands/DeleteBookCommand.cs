using MediatR;

namespace BookstoreApi.Application.Books.Commands
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public int BookId { get; set; }
    }
}
