using Bk.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Queries.BookEntries
{
    public class GetBookEntriesHandler : IRequestHandler<GetBookEntries, GetBookEntriesResponse>
    {
        private readonly AppDbContext _context;
        public GetBookEntriesHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetBookEntriesResponse> Handle(GetBookEntries request, CancellationToken cancellationToken)
        {
            if(request.Page == null)
            {
                var count = await _context.BookEntries.CountAsync(x => x.BookId == request.BookId);
                request.Page = count / request.Limit;
                if(count % request.Limit != 0)
                {
                    request.Page++;
                }
            }

            return new GetBookEntriesResponse
            {
                Entries = await _context.BookEntries
                .Where(x => x.BookId == request.BookId)
                .Select(x => new BookEntryResponse
                {
                    Id = x.Id,
                    Description = x.Description,
                    Date = x.Date.Date.ToString(),
                    Debit = x.Debit,
                    Credit = x.Credit,
                    Driver = x.Driver,
                    Vehicle = x.Vehicle,
                    Litre = x.Litre,
                    ItemType = x.ItemType.ToString(),
                    Amount = x.Amount,
                    Balance = _context.BookEntries.Where(a=>a.Id <= x.Id).Sum(x => x.Credit) - _context.BookEntries.Where(a => a.Id <= x.Id).Sum(x => x.Debit),
                    CreatedBy = _context.Users.FirstOrDefault(a => a.Id == x.CreatedBy).UserName,
                    CreatedOn = x.CreatedOn.ToString(),
                    UpdatedBy = x.CreatedOn == x.UpdatedOn ? null : _context.Users.FirstOrDefault(a => a.Id == x.UpdatedBy).UserName,
                    UpdatedOn = x.CreatedOn == x.UpdatedOn ? null : x.UpdatedOn.ToString(),

                })
                .OrderBy(x => x.Id)
                .Skip((request.Page.Value-1) * request.Limit.Value)
                .Take(request.Limit.Value)
                .ToListAsync()
            };
        }
    }
}
