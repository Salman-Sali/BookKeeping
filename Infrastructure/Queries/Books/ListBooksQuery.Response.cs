using Bk.Infrastructure.Extentions;

namespace Bk.Infrastructure.Queries.Books
{
    public class ListBooksQueryResponse : PaginationResponse
    {
        public IEnumerable<BookResponse>? Books { get; set; }
        public int TotalPages { get; set; }
    }

    public class BookResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string BookType { get; set; }
        public float? Balance { get; set; }
        public float? DiscountPerLitre { get; set; }

        public string CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
    }
}
