using MediatR;

namespace BK.Application.Commands.BookEntries
{
    public class UpdateBookEntryCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public int BookEntryId { get; set; }

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
