using MediatR;

namespace Bk.Infrastructure.Queries.BookEntries
{
    public class ListVehiclesQuery : IRequest<ListVehiclesQueryResponse>
    {
        public int BookId { get; set; }
    }
}
