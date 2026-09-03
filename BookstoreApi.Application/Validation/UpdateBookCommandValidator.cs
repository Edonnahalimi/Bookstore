using BookstoreApi.Application.Books.Commands;
using FluentValidation;

namespace BookstoreApi.Application.Validation
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0);

            RuleFor(x => x.AuthorId)
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(x => x.SubTitle)
           .MaximumLength(200);
        }
    }
}
