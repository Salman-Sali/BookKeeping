using Bk.Domain.Entities;
using Bk.Domain.Enums;
using Bk.Infrastructure.Extentions;

namespace Bk.Infrastructure.Queries.BookEntries
{
    public class GetBookEntriesResponse : PaginationResponse
    {
        public List<BookEntryResponse>? Entries { get; set; }
        public int TotalPages { get; set; }
    }

    public class BookEntryResponse
    {
        public long Id { get; set; }
        public int BookId { get; set; }

        public long SerialNumber { get; set; }

        public string? Description { get; set; }

        public string Date { get; set; }
        public float? Debit { get; set; }
        public float? Credit { get; set; }


        public string? Driver { get; set; }
        public string? Vehicle { get; set; }
        public float? Litre { get; set; }
        public string? ItemType { get; set; }

        public float? Amount { get; set; }

        public float? Balance { get; set; }

        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
    }
}
