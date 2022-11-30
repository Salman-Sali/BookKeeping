using Bk.Domain.Enums;
using MediatR;

namespace BK.Application.Commands.BookEntries
{
    public class AddBookEntryCommand : IRequest<bool>
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public string? Description { get; set; }

        public string Date { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }


        public string? Driver { get; set; }
        public string? Vehicle { get; set; }
        public decimal? Litre { get; set; }
        public decimal? Amount { get; set; }
        public string? ItemType { get; set; }
    }
}
