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
        public float? Debit { get; set; }
        public float? Credit { get; set; }


        public string? Driver { get; set; }
        public string? Vehicle { get; set; }
        public float? Litre { get; set; }
        public float? Amount { get; set; }
        public string? ItemType { get; set; }
    }
}
