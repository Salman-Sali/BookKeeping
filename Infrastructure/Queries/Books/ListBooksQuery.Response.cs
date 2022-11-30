namespace Bk.Infrastructure.Queries.Books
{
    public class ListBooksQueryResponse
    {
        public IEnumerable<BookResponse>? Books { get; set; }
    }

    public class BookResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BookType { get; set; }
        public decimal? Balance { get; set; }
    }
}
