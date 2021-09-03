using AutoMapper;
using BookStore.Authors.Api.Application.ModelsDto;
using BookStore.Authors.Api.Models;
using BookStore.Authors.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Authors.Api.Application.AuthorFeatures.Queries
{
    public class AuthorsListQuery: IRequest<List<AuthorDto>> { }

    public class AuthorsListQueryHandler : IRequestHandler<AuthorsListQuery, List<AuthorDto>>
    {
        private readonly ContextAuthor _context;
        private readonly IMapper _mapper;

        public AuthorsListQueryHandler(ContextAuthor context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AuthorDto>> Handle(AuthorsListQuery request, CancellationToken cancellationToken)
        {
            var authorList = await _context.Author.ToListAsync();
            var authorListDto = _mapper.Map<List<Author>, List<AuthorDto>>(authorList);
            return authorListDto;
        }
    }
}
