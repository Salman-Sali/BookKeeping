using MediatR;

namespace Bk.Infrastructure.Queries.Books
{
    public class GetBookByIdQuery : IRequest<BookResponse>
    {
        public int BookId { get; set; }
    }
}
