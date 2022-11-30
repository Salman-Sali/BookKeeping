using MediatR;

namespace BK.Application.Commands.Books
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public int BookId { get; set; }
    }
}
