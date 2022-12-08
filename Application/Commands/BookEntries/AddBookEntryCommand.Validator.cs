using Bk.Domain.Enums;
using FluentValidation;
using Bk.Infrastructure.Context;

namespace BK.Application.Commands.BookEntries
{
    public class AddBookEntryCommandValidator : AbstractValidator<AddBookEntryCommand>
    {
        public AddBookEntryCommandValidator(AppDbContext context)
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .Must(x => context.Users.FirstOrDefault(a => a.Id == x) != null)
                .WithMessage("User not found.");

            RuleFor(x => x.BookId)
                .GreaterThan(0)
                .Must(x => context.Books.FirstOrDefault(a => a.Id == x) != null)
                .WithMessage("Book not found.");

            RuleFor(x => x.Description)
                .MaximumLength(100)
                .When(x => x.Description != null);

            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(x => DateTime.TryParse(x, out DateTime result))
                .WithMessage("Please give a valid date.");

            RuleFor(x => x)
                .Must(x =>
                {
                    if ((x.Credit == null && x.Debit == null) || (x.Debit != null && x.Credit != null))
                    {
                        return false;
                    }
                    return true;
                })
                .WithMessage("Credit or debit. Not both.");

            RuleFor(x => x.Driver)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30)
                .When(x => x.Driver != null);

            RuleFor(x => x.Vehicle)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30)
                .When(x => x.Vehicle != null);

            RuleFor(x => x.Litre)
                .GreaterThan(0)
                .When(x => x.Litre.HasValue);

            RuleFor(x => x.ItemType)
                .NotEmpty()
                .Must(x=> Enum.TryParse(typeof(ItemType), x, out var result))
                .WithMessage("Provide a valid item type.")
                .When(x => !string.IsNullOrEmpty(x.ItemType));

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .When(x => x.Amount.HasValue);
        }
    }
}
