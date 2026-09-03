using BookstoreApi.Application.Common;
using BookstoreApi.Application.DTOs;
using BookstoreApi.Application.IRepositories;
using MediatR;

namespace BookstoreApi.Application.Books.Queries
{
    public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, PagedResult<BookDto>>
    {
        private readonly IBookRepository _repository;

        public SearchBooksQueryHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<BookDto>> Handle(SearchBooksQuery request, CancellationToken cancellationToken)
        {
            var (books, totalCount) = await _repository.SearchAsync(request, cancellationToken);

            var items = books.Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                SubTitle = b.SubTitle,
                Author = new AuthorDto
                {
                    AuthorId = b.Author.AuthorId,
                    Name = b.Author.Name
                }
            }).ToList();

            return new PagedResult<BookDto>
            {
                Items = items,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
}
