using MediatR;

namespace BookstoreApi.Application.Books.Commands
{
    public class CreateBookCommand : IRequest<int>
    {
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string? SubTitle { get; set; }
    }
}
