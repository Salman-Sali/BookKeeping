using Bk.Domain.Enums;

namespace Bk.Domain.Entities.BookTypeEntryEntities
{
    public interface FuelCreditBookEntry
    {
        public string? Driver { get; set; }
        public string? Vehicle { get; set; }
        public float? Litre { get; set; }
        public ItemType? ItemType { get; set; }
    }
}
