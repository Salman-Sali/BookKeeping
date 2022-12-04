using MediatR;

namespace Bk.Infrastructure.Queries.BookEntries
{
    public class GetBookEntries : IRequest<GetBookEntriesResponse>
    {
        public int BookId { get; set; }
        public int? Page { get; set; }
        public int? Limit { get; set; } = 15;
    }
}
