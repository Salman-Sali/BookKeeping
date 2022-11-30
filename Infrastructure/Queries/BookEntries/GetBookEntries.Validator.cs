using Bk.Infrastructure.Context;
using FluentValidation;

namespace Bk.Infrastructure.Queries.BookEntries
{
    public class GetBookEntriesValidator : AbstractValidator<GetBookEntries>
    {
        public GetBookEntriesValidator(AppDbContext context)
        {
            RuleFor(x=>x.BookId)
                .GreaterThan(0)
                .Must(x=> context.Books.FirstOrDefault(a=>a.Id == x) != null)
                .WithMessage("Book not found.");

            RuleFor(x => x.Page)
                .GreaterThan(0)
                .When(x=>x.Page.HasValue);

            RuleFor(x => x.Limit)
                .GreaterThan(0)
                .When(x=>x.Limit.HasValue);
        }
    }
}
