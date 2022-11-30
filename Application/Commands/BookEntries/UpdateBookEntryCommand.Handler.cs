using Bk.Domain.Entities;
using Bk.Domain.Enums;
using Bk.Infrastructure.Context;
using Bk.Shared;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BK.Application.Commands.BookEntries
{
    public class UpdateBookEntryCommandHandler : IRequestHandler<UpdateBookEntryCommand, bool>
    {
        private readonly AppDbContext _context;
        public UpdateBookEntryCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateBookEntryCommand request, CancellationToken cancellationToken)
        {
            var bookEntry = _context.BookEntries.FirstOrDefault(x => x.Id == request.BookEntryId);
            if (bookEntry == null)
            {
                throw new Exception("Entry not found.");
            }
            bookEntry = ObjectToObjectMapper<UpdateBookEntryCommand, BookEntry>.Map(request, bookEntry);
            bookEntry.ItemType = request.ItemType != null ? (ItemType)Enum.Parse(typeof(ItemType), request.ItemType) : null;
            bookEntry.Date = DateTime.Parse(request.Date);
            bookEntry.SetUpdatedBy(request.UserId);
            _context.Update(bookEntry);
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            throw new Exception("Update entry failed.");
        }
    }
}
