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
        public float? Debit { get; set; }
        public float? Credit { get; set; }


        public string? Driver { get; set; }
        public string? Vehicle { get; set; }
        public float? Litre { get; set; }
        public ItemType? ItemType { get; set; }

        public float? Amount { get; set; }
    }
}
