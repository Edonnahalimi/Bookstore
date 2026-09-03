using BookstoreApi.Domain.Entities;

namespace BookstoreApi.Application.DTOs
{
    public class BookDto
    {
        public int BookId { get; set; }
        public AuthorDto Author { get; set; }
        public string Title { get; set; }
        public string? SubTitle { get; set; }
    }
}