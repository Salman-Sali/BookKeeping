using MediatR;

namespace Bk.Infrastructure.Queries.Books
{
    public class ListBooksQuery : IRequest<ListBooksQueryResponse>
    {
        public string? BookType { get; set; }
        public int? Page { get; set; } = 1;
        public int? Limit { get; set; } = 15;
    }
}
