using BookstoreApi.Application.DTOs;
using MediatR;

namespace BookstoreApi.Application.Books.Queries
{
    public class GetBooksQuery : IRequest<List<BookDto>>
    {
    }
}
