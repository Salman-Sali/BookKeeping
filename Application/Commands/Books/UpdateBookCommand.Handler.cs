using Bk.Domain.Entities;
using Bk.Infrastructure.Context;
using MediatR;
using Bk.Shared;

namespace BK.Application.Commands.Books
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly AppDbContext _context;
        public UpdateBookCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == request.BookId);
            if(book == null)
            {
                throw new Exception("Cannot find book.");
            }
            book = ObjectToObjectMapper<UpdateBookCommand, Book>.Map(request, book);
            book.SetUpdatedBy(request.UserId);
            _context.Update(book);
            if(await _context.SaveChangesAsync() > 0) {
                return true;
            }
            throw new Exception("Updating book failed.");
        }
    }
}
