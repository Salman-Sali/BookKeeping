using MediatR;

namespace BK.Application.Commands.Books
{
    public class UpdateBookCommand : IRequest<bool>
    {
        public int UserId { get; set; }

        public int BookId { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
    }
}
