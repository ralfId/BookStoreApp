using AutoMapper;
using BookStore.Authors.Api.Application.ModelsDto;
using BookStore.Authors.Api.Models;
using BookStore.Authors.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Authors.Api.Application.AuthorFeatures.Queries
{
    public class AuthorByIdQuery : IRequest<AuthorDto>
    {
        public string AuthorGuid { get; set; }
    }

    public class AuthorByIdQueryHandler : IRequestHandler<AuthorByIdQuery, AuthorDto>
    {
        private readonly ContextAuthor _context;
        private readonly IMapper _mapper;
        public AuthorByIdQueryHandler(ContextAuthor context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuthorDto> Handle(AuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Author.Where(a => a.AuthorGuid == request.AuthorGuid).FirstOrDefaultAsync();

            if (author != null)
            {
                var authorDto = _mapper.Map<Author, AuthorDto>(author);
                return authorDto;
            }
            else
            {
                throw new Exception("Can´t find the author");
            }
        }
    }
}
