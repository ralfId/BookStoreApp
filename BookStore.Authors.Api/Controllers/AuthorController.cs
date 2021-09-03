using BookStore.Authors.Api.Application.AuthorFeatures.Commands;
using BookStore.Authors.Api.Application.AuthorFeatures.Queries;
using BookStore.Authors.Api.Application.ModelsDto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Authors.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public AuthorController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateAuthor(CreateAuthorCommand author)
        {
            return await _mediatR.Send(author);
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAuthorsList()
        {
            return await _mediatR.Send(new AuthorsListQuery());
        }

        [HttpGet("{authorId}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(string authorId)
        {
            return await _mediatR.Send(new AuthorByIdQuery { AuthorGuid = authorId });
        }
    }
}
