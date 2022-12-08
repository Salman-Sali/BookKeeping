using Bk.Domain.Entities;
using Bk.Domain.Enums;
using Bk.Infrastructure.Context;
using MediatR;
using Bk.Shared;

namespace BK.Application.Commands.BookEntries
{
    public class AddBookEntryCommandHandler : IRequestHandler<AddBookEntryCommand, bool>
    {
        private readonly AppDbContext _context;
        public AddBookEntryCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddBookEntryCommand request, CancellationToken cancellationToken)
        {
            var bookEntry = new BookEntry(request.UserId)
            {
                Date = DateTime.Parse(request.Date),
                ItemType = !string.IsNullOrWhiteSpace(request.ItemType)? (ItemType)Enum.Parse(typeof(ItemType), request.ItemType) : null,
            };
            bookEntry = ObjectToObjectMapper<AddBookEntryCommand, BookEntry>.Map(request, bookEntry);
            await _context.AddAsync(bookEntry);
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            throw new Exception("Book entry failed.");
        }
    }
}
