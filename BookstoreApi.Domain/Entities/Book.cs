namespace BookstoreApi.Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
    }
}
