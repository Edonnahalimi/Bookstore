using BookstoreApi.Application.Books.Commands;
using BookstoreApi.Application.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers;

[ApiController]
[Route("v1/books")]
public class BooksController : ControllerBase
{
    private readonly ISender _sender;

    public BooksController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    [Authorize(Policy = "BookCrud")]
    public async Task<IActionResult> GetBooks(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetBooksQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = "BookCrud")]
    public async Task<IActionResult> GetBook(int id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetBookByIdQuery { BookId = id }, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("search")]
    [Authorize(Policy = "BookSearch")]
    public async Task<IActionResult> Search([FromQuery] SearchBooksQuery query, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "BookCrud")]
    public async Task<IActionResult> Create(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var id = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetBook), new { id }, id);
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "BookCrud")]
    public async Task<IActionResult> Update(int id, UpdateBookCommand command, CancellationToken cancellationToken)
    {
        command.BookId = id;
        var result = await _sender.Send(command, cancellationToken);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "BookCrud")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteBookCommand { BookId = id }, cancellationToken);

        if (!result)
            return NotFound();

        return NoContent();
    }
}
