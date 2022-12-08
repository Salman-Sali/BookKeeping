using Bk.Domain.Enums;
using Bk.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Queries.Books
{
    public class ListBooksQueryHandler : IRequestHandler<ListBooksQuery, ListBooksQueryResponse>
    {
        private readonly AppDbContext _context;
        public ListBooksQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ListBooksQueryResponse> Handle(ListBooksQuery request, CancellationToken cancellationToken)
        {
            BookType? bookType = !string.IsNullOrEmpty(request.BookType) ? (BookType)Enum.Parse(typeof(BookType), request.BookType) : null;

            int totalPages = 0;
            var count = await _context.Books.CountAsync(x => bookType != null ? x.BookType == bookType : true);
            
            totalPages = count / request.Limit.Value;
            if (count % request.Limit != 0)
            {
                totalPages++;
            }
            return new ListBooksQueryResponse
            {
                Books = await _context
                .Books
                .Where(x => bookType != null ? x.BookType == bookType : true)
                .Select(x => new BookResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    BookType = x.BookType.ToString(),
                    Phone = x.Phone,
                    Balance = _context.BookEntries.Where(a=>a.BookId == x.Id).Sum(x => x.Credit) - _context.BookEntries.Where(a => a.BookId == x.Id).Sum(x => x.Debit),
                    DiscountPerLitre = x.DiscountPerLitre,
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
