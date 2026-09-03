using BookstoreApi.Application.Authors.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("v1/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ISender _sender;
        public AuthorsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Authorize(Policy = "BookCrud")]
        public async Task<IActionResult> Create([FromBody] CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            var id = await _sender.Send(command, cancellationToken);
            return Ok(id);
        }
    }
}
