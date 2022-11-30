using Bk.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BK.Application.Commands.Books
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteBookCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            if(await _context.Books.Where(x=>x.Id == request.BookId).ExecuteDeleteAsync() > 0)
            {
                return true;
            }
            throw new Exception("Book delete failed.");
        }
    }
}
