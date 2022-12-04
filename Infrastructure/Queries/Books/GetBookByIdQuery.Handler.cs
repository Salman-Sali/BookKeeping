using Bk.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Queries.Books
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookResponse>
    {
        private readonly AppDbContext _context;
        public GetBookByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BookResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Books
                .Select(x=> new BookResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    BookType = x.BookType.ToString(),
                    Phone = x.Phone,
                    Balance = _context.BookEntries.Where(a => a.BookId == x.Id).Sum(x => x.Credit) - _context.BookEntries.Where(a => a.BookId == x.Id).Sum(x => x.Debit),
                    CreatedBy = _context.Users.FirstOrDefault(a => a.Id == x.CreatedBy).UserName,
                    CreatedOn = x.CreatedOn.ToShortDateString() + " " + x.CreatedOn.ToShortTimeString(),
                    UpdatedBy = (x.CreatedOn.ToShortDateString() == x.UpdatedOn.ToShortDateString() && x.CreatedOn.ToShortTimeString() == x.UpdatedOn.ToShortTimeString()) ? null : _context.Users.FirstOrDefault(a => a.Id == x.UpdatedBy).UserName,
                    UpdatedOn = x.CreatedOn.ToShortDateString() == x.UpdatedOn.ToShortDateString() && x.CreatedOn.ToShortTimeString() == x.UpdatedOn.ToShortTimeString() ? null : x.UpdatedOn.ToShortDateString() + " " + x.UpdatedOn.ToShortTimeString(),
                })
                .FirstOrDefaultAsync(x => x.Id == request.BookId);
        }
    }
}
