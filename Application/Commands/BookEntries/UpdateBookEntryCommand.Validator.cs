using Bk.Domain.Enums;
using Bk.Infrastructure.Context;
using FluentValidation;

namespace BK.Application.Commands.BookEntries
{
    public class UpdateBookEntryCommandValidator : AbstractValidator<UpdateBookEntryCommand>
    {
        public UpdateBookEntryCommandValidator(AppDbContext context)
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .Must(x => context.Users.FirstOrDefault(a => a.Id == x) != null)
                .WithMessage("User not found.");

            RuleFor(x => x.BookEntryId)
                .GreaterThan(0)
                .Must(x => context.BookEntries.FirstOrDefault(a => a.Id == x) != null)
                .WithMessage("Entry not found.");

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
                .When(x => !string.IsNullOrEmpty(x.Driver));

            RuleFor(x => x.Vehicle)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30)
                .When(x => !string.IsNullOrEmpty(x.Vehicle));

            RuleFor(x => x.Litre)
                .GreaterThan(0)
                .When(x => x.Litre.HasValue);

            RuleFor(x => x.ItemType)
                .NotEmpty()
                .Must(x => Enum.TryParse(typeof(ItemType), x, out var result))
                .WithMessage("Provide a valid item type.")
                .When(x => !string.IsNullOrEmpty(x.ItemType));

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .When(x => x.Amount.HasValue);
        }
    }
}
