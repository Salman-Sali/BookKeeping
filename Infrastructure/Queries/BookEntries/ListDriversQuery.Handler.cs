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

        public Task<ListDriversQueryResponse> Handle(ListDriversQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ListDriversQueryResponse
            {
                Drivers = _context.BookEntries.Where(x => x.Id == request.BookId).Select(x => x.Driver).ToList().Distinct().ToList(),
            });
        }
    }
}
