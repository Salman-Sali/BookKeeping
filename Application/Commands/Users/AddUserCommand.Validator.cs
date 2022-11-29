using FluentValidation;
using Infrastructure.Context;

namespace Application.Commands.Users
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator(AppDbContext context)
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .Must(x=> context.Users.FirstOrDefault(a=>a.Id == x) != null)
                .WithMessage("User adding not found.");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30)
                .Must(x => context.Users.FirstOrDefault(a => a.UserName == x) == null)
                .WithMessage("User with same name already exist.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30);
        }
    }
}
