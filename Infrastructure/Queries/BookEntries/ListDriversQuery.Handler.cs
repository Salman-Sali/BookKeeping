using Bk.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Queries.BookEntries
{
    public class ListDriversQueryHandler : IRequestHandler<ListDriversQuery, ListDriversQueryResponse>
    {
        private readonly AppDbContext _context;
        public ListDriversQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ListDriversQueryResponse> Handle(ListDriversQuery request, CancellationToken cancellationToken)
        {
            return new ListDriversQueryResponse
            {
                Drivers = await _context.BookEntries.Where(x => x.BookId == request.BookId).Select(x => x.Driver).Distinct().ToListAsync(),
            };
        }
    }
}
