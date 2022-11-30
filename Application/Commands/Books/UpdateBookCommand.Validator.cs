using FluentValidation;
using Bk.Infrastructure.Context;

namespace BK.Application.Commands.Books
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator(AppDbContext context)
        {
            RuleFor(x=>x.UserId)
                .GreaterThan(0)
                .Must(x=> context.Users.FirstOrDefault(a=>a.Id == x) != null)
                .WithMessage("User not found.");

            RuleFor(x => x.BookId)
                .GreaterThan(0)
                .Must(x => context.Books.FirstOrDefault(a => a.Id == x) != null)
                .WithMessage("Book not found.");

            RuleFor(x => x.Phone)
                .Length(10)
                .When(x => x.Phone != null);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(30);

            RuleFor(x=>x)
                .Must(x=> context.Books.FirstOrDefault(a=>a.Id != x.BookId && a.Name == x.Name) == null)
                .WithMessage("Book with same name already exist.")
                .When(x=>x.Name.Length >= 4 && x.Name.Length <= 30);
        }
    }
}
