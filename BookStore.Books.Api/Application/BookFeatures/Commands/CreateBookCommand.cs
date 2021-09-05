using BookStore.Books.Api.Models;
using BookStore.Books.Api.Persistence;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Books.Api.Application.BookFeatures.Commands
{
    public class CreateBookCommand :  IRequest
    {
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public Guid? Author { get; set; }
    }

    public class CreateBookValidation : AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidation()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.PublicationDate).NotEmpty();
            RuleFor(x => x.Author).NotEmpty();
        }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
    {
        private readonly ContextBook _context;

        public CreateBookCommandHandler(ContextBook context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var newBook = new Book
            {
                Title = request.Title,
                PublicationDate = request.PublicationDate,
                AuthoBook = request.Author
            };

            _context.Book.Add(newBook);
            var dbresp = await _context.SaveChangesAsync();

            if (dbresp > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new Exception("Can´t create the new book");
            }
        }
    }
}
