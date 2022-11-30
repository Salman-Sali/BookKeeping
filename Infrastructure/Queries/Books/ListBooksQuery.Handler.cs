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
            return new ListBooksQueryResponse
            {
                Books = await _context
                .Books
                .Where(x => request.BookType != null ? x.BookType.ToString() == request.BookType : true)
                .Select(x => new BookResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    BookType = x.BookType.ToString(),
                    Balance = _context.BookEntries.Sum(x => x.Credit) - _context.BookEntries.Sum(x => x.Debit)
                })
                .ToListAsync()
            };
        }
    }
}
