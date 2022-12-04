using MediatR;

namespace Bk.Infrastructure.Queries.BookEntries
{
    public class ListDriversQuery : IRequest<ListDriversQueryResponse>
    {
        public int BookId { get; set; }
    }
}
