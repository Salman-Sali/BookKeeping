using MediatR;

namespace BK.Application.Commands.Books
{
    public class AddBookCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string BookType { get; set; }
        public decimal? DiscountPerLitre { get; set; }

        public int UserId { get; set; }
    }
}
