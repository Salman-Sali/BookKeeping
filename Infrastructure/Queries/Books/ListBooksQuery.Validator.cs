using Bk.Domain.Enums;
using FluentValidation;

namespace Bk.Infrastructure.Queries.Books
{
    public class ListBooksQueryValidator : AbstractValidator<ListBooksQuery>
    {
        public ListBooksQueryValidator()
        {
            RuleFor(x => x.BookType)
                .NotEmpty()
                .Must(x => Enum.TryParse(typeof(BookType), x, out var result))
                .WithMessage("Invalid book type.")
                .When(x => x.BookType != null);
        }
    }
}
