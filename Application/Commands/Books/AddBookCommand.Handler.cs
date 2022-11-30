using Bk.Domain.Entities;
using Bk.Domain.Enums;
using Bk.Infrastructure.Context;
using MediatR;
using Bk.Shared;

namespace BK.Application.Commands.Books
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, bool>
    {
        private readonly AppDbContext _context;
        public AddBookCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book(request.UserId)
            {
                BookType = (BookType)Enum.Parse(typeof(BookType), request.BookType)
            };
            book = ObjectToObjectMapper<AddBookCommand, Book>.Map(request, book);
            await _context.AddAsync(book, cancellationToken);
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            throw new Exception("Add book failed.");
        }
    }
}
