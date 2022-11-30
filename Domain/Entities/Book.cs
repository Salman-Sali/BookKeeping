using Bk.Domain.Entities.BookEntities;
using Bk.Domain.Enums;

namespace Bk.Domain.Entities
{
    public class Book : BaseEntity<int>, FuelDiscountCreditBook
    {
        private Book()
        {

        }
        public Book(int createdBy) : base(createdBy)
        {

        }

        public string Name { get; set; }//make unique and index
        public string? Phone { get; set; }
        public BookType BookType { get; set; }

        public decimal? DiscountPerLitre { get; set; }

        public IEnumerable<BookEntry> BookEntries { get; set; }
    }
}
