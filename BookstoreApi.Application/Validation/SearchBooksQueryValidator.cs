using BookstoreApi.Application.Books.Queries;
using FluentValidation;

namespace BookstoreApi.Application.Validation
{
    public class SearchBooksQueryValidator : AbstractValidator<SearchBooksQuery>
    {
        public SearchBooksQueryValidator()
        {
            RuleFor(x => x.Title).MaximumLength(100);

            RuleFor(x => x.Author).MaximumLength(100);

            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
