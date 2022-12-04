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
            int totalPages = 0;
            var count = await _context.BookEntries.CountAsync(x => x.BookId == request.BookId);
            totalPages = count / request.Limit.Value;
            if (count % request.Limit != 0)
            {
                totalPages++;
            }
            if (request.Page == null)
            {
                request.Page = count / request.Limit;
                if (count % request.Limit != 0)
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
                    BookId = x.BookId,
                    SerialNumber = _context.BookEntries.Where(a=>a.BookId == request.BookId && a.Id <= x.Id).Count(),
                    Description = x.Description,
                    Date = x.Date.ToShortDateString(),
                    Debit = x.Debit,
                    Credit = x.Credit,
                    Driver = x.Driver,
                    Vehicle = x.Vehicle,
                    Litre = x.Litre,
                    ItemType = x.ItemType.ToString(),
                    Amount = x.Amount,
                    Balance = _context.BookEntries.Where(a => a.Id <= x.Id && a.BookId == request.BookId).Sum(x => x.Credit) - _context.BookEntries.Where(a => a.Id <= x.Id && a.BookId == request.BookId).Sum(x => x.Debit),
                    CreatedBy = _context.Users.FirstOrDefault(a => a.Id == x.CreatedBy).UserName,
                    CreatedOn = x.CreatedOn.ToShortDateString() + " " + x.CreatedOn.ToShortTimeString(),
                    UpdatedBy = (x.CreatedOn.ToShortDateString() == x.UpdatedOn.ToShortDateString() && x.CreatedOn.ToShortTimeString() == x.UpdatedOn.ToShortTimeString()) ? null : _context.Users.FirstOrDefault(a => a.Id == x.UpdatedBy).UserName,
                    UpdatedOn = x.CreatedOn.ToShortDateString() == x.UpdatedOn.ToShortDateString() && x.CreatedOn.ToShortTimeString() == x.UpdatedOn.ToShortTimeString() ? null : x.UpdatedOn.ToShortDateString() + " " + x.UpdatedOn.ToShortTimeString(),

                })
                .OrderBy(x => x.Id)
                .Skip((request.Page.Value - 1) * request.Limit.Value)
                .Take(request.Limit.Value)
                .ToListAsync(),
                TotalPages = totalPages
            };
        }
    }
}
