using BookstoreApi.Application.Authors.Commands;
using FluentValidation;

namespace BookstoreApi.Application.Validation
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);
        }
    }
}
