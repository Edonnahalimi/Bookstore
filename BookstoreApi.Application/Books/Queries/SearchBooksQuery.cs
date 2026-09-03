using BookstoreApi.Application.Common;
using BookstoreApi.Application.DTOs;
using MediatR;

namespace BookstoreApi.Application.Books.Queries
{
    public class SearchBooksQuery : IRequest<PagedResult<BookDto>>
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
