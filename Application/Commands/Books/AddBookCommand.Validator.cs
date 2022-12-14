using Bk.Domain.Enums;
using FluentValidation;
using Bk.Infrastructure.Context;

namespace BK.Application.Commands.Books
{
    public class AddBookCommandValidator : AbstractValidator<AddBookCommand>
    {
        public AddBookCommandValidator(AppDbContext context)
        {
            RuleFor(x=>x.UserId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .Must(x=> context.Users.FirstOrDefault(a=>a.Id == x) != null)
                .WithMessage("User not found.");

            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30)
                .Must(x => context.Books.FirstOrDefault(a => a.Name == x) == null)
                .WithMessage("Book with same name already exists.");

            RuleFor(x => x.Phone)
                .Length(10)
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.BookType)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(x => Enum.TryParse(typeof(BookType), x, out var result))
                .WithMessage("Book type selected is not valid.");

            RuleFor(x => x)
                .Must(x => x.DiscountPerLitre != null)
                .WithMessage("Provide a value for discount per litre.")
                .When(x => x.BookType == BookType.FuelCreditDiscountBook.ToString());
        }
    }
}
