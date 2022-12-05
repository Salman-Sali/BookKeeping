using Bk.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Queries.BookEntries
{
    public class ListVehiclesQueryHandler : IRequestHandler<ListVehiclesQuery, ListVehiclesQueryResponse>
    {
        private readonly AppDbContext _context;
        public ListVehiclesQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ListVehiclesQueryResponse> Handle(ListVehiclesQuery request, CancellationToken cancellationToken)
        {
            return new ListVehiclesQueryResponse
            {
                Vehicles = await _context.BookEntries.Where(x => x.BookId == request.BookId).Select(x => x.Vehicle).Distinct().ToListAsync(),
            };
        }
    }
}
