using Bk.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BK.Application.Commands.BookEntries
{
    public class DeleteBookEntryCommandHandler : IRequestHandler<DeleteBookEntryCommand, bool>
    {
        private readonly AppDbContext _context;
        public DeleteBookEntryCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteBookEntryCommand request, CancellationToken cancellationToken)
        {
            if(await _context.BookEntries.Where(x=>x.Id == request.BookEntryId).ExecuteDeleteAsync() > 0)
            {
                return true;
            }
            throw new Exception("Entry delete failed.");
        }
    }
}
