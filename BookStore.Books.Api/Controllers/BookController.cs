using BookStore.Books.Api.Application.BookFeatures.Commands;
using BookStore.Books.Api.Application.BookFeatures.Queries;
using BookStore.Books.Api.Application.ModelsDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateBook(CreateBookCommand book)
        {
            return await _mediator.Send(book);
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks()
        {
            return await _mediator.Send(new AllBooksListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(Guid id)
        {
            return await _mediator.Send(new BookByIdQuery { BookId = id });
        }
    }
}
