using MediatR;

namespace Bk.Infrastructure.Queries.Books
{
    public class ListBooksQuery : IRequest<ListBooksQueryResponse>
    {
        public string? BookType { get; set; }
    }
}
