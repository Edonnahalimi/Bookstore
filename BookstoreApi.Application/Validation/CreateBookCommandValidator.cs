using BookstoreApi.Application.Books.Commands;
using FluentValidation;

namespace BookstoreApi.Application.Validation
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.AuthorId)
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(x => x.SubTitle)
                .MaximumLength(200)
                .When(x => x.SubTitle != null);
        }
    }
}
