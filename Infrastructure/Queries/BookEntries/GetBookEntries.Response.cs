using Bk.Domain.Entities;
using Bk.Domain.Enums;

namespace Bk.Infrastructure.Queries.BookEntries
{
    public class GetBookEntriesResponse
    {
        public List<BookEntryResponse>? Entries { get; set; }
    }

    public class BookEntryResponse
    {
        public long Id { get; set; }

        public string? Description { get; set; }

        public string Date { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }


        public string? Driver { get; set; }
        public string? Vehicle { get; set; }
        public decimal? Litre { get; set; }
        public string? ItemType { get; set; }

        public decimal? Amount { get; set; }

        public decimal? Balance { get; set; }

        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
    }
}
