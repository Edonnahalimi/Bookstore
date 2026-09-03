using BookstoreApi.Application.IRepositories;
using BookstoreApi.Domain.Entities;
using MediatR;

namespace BookstoreApi.Application.Authors.Commands
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
    {
        private readonly IAuthorRepository _repository;

        public CreateAuthorCommandHandler(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Name = request.Name
            };

            await _repository.AddAsync(author, cancellationToken);

            return author.AuthorId;
        }
    }
}
