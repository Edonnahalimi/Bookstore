using MediatR;

namespace BookstoreApi.Application.Authors.Commands
{
    public class CreateAuthorCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
