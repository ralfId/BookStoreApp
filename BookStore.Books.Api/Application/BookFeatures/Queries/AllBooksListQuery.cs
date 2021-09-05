using AutoMapper;
using BookStore.Books.Api.Application.ModelsDto;
using BookStore.Books.Api.Models;
using BookStore.Books.Api.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Books.Api.Application.BookFeatures.Queries
{
    public class AllBooksListQuery : IRequest<List<BookDto>>
    {
    }

    public class AllBooksListQueryHandler : IRequestHandler<AllBooksListQuery, List<BookDto>>
    {
        private readonly ContextBook _context;
        private readonly IMapper _mapper;

        public AllBooksListQueryHandler(ContextBook context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> Handle(AllBooksListQuery request, CancellationToken cancellationToken)
        {
            var bookList = await _context.Book.ToListAsync();
            var bookListMap = _mapper.Map<List<Book>, List<BookDto>>(bookList);

            return bookListMap;
        }
    }
}
