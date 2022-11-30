using Bk.Domain.Entities.BookTypeEntryEntities;
using Bk.Domain.Enums;

namespace Bk.Domain.Entities
{
    public class BookEntry : BaseEntity<long>, FuelCreditBookEntry, FuelDiscountCreditBookEntry
    {
        private BookEntry()
        {

        }
        public BookEntry(int createdBy) : base(createdBy)
        {

        }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public string? Description { get; set; }

        public DateTime Date { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }


        public string? Driver { get; set; }
        public string? Vehicle { get; set; }
        public decimal? Litre { get; set; }
        public ItemType? ItemType { get; set; }

        public decimal? Amount { get; set; }
    }
}
