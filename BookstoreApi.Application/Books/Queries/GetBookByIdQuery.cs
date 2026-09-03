using BookstoreApi.Application.DTOs;
using MediatR;

namespace BookstoreApi.Application.Books.Queries
{
    public class GetBookByIdQuery : IRequest<BookDto?>
    {
        public int BookId { get; set; }
    }
}
