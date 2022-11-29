using FluentValidation;
using Infrastructure.Context;

namespace Application.Commands.Users
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(AppDbContext context)
        {
            RuleFor(x => x.UpdaterUserId)
                .GreaterThan(0)
                .Must(x => context.Users.FirstOrDefault(a => a.Id == x) != null)
                .WithMessage("Updater user not found.");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .Must(x => context.Users.FirstOrDefault(a => a.Id == x) != null)
                .WithMessage("User not found.");

            RuleFor(x => x.UserName)
                .MinimumLength(4)
                .MaximumLength(30);

            RuleFor(x => x.Password)
                .MinimumLength(4)
                .MaximumLength(30);

            RuleFor(x=>x)
                .Must(x=> context.Users.FirstOrDefault(a=>a.Id != x.UserId && a.UserName == x.UserName) == null)
                .WithMessage("User with user name already exists.")
                .When(x=> x.UserName.Length >= 4 && x.UserName.Length <= 30);
        }
    }
}
