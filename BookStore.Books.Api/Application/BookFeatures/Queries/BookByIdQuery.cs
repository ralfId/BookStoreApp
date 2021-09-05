using AutoMapper;
using BookStore.Books.Api.Application.ModelsDto;
using BookStore.Books.Api.Models;
using BookStore.Books.Api.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Books.Api.Application.BookFeatures.Queries
{
    public class BookByIdQuery : IRequest<BookDto>
    {
        public Guid BookId { get; set; }
    }

    public class BookByIdValidation : AbstractValidator<BookByIdQuery>
    {
        public BookByIdValidation()
        {
            RuleFor(x => x.BookId).NotEmpty();
        }
    }

    public class BookByIdQueryHandler : IRequestHandler<BookByIdQuery, BookDto>
    {
        private readonly ContextBook _context;
        private readonly IMapper _mapper;
        public BookByIdQueryHandler(ContextBook context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(BookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Book.Where(x => x.BookId == request.BookId).FirstOrDefaultAsync();

            if (book != null)
            {
                var bookMap = _mapper.Map<Book, BookDto>(book);
                return bookMap;
            }
            else
            {
                throw new Exception("Can´t find the book");
            }
        }
    }
}
