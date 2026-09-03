using MediatR;

namespace BookstoreApi.Application.Books.Commands
{
    public class UpdateBookCommand : IRequest<bool>
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string? SubTitle { get; set; }
    }
}
