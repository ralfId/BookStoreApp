using BookStore.Authors.Api.Models;
using BookStore.Authors.Api.Persistence;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Authors.Api.Application.AuthorFeatures.Commands
{
    public class CreateAuthorCommand : IRequest
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime? Birthday { get; set; }
    }

    public class CreateAuthorValidation : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorValidation()
        {
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.Lastname).NotEmpty();
        }
    }

    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand>
    {
        private readonly ContextAuthor _context;
        public CreateAuthorCommandHandler(ContextAuthor context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {

            var newAuthor = new Author
            {
                AuthorGuid = Guid.NewGuid().ToString(),
                Name = request.Name,
                Lastname = request.Lastname,
                Birthday = request.Birthday
            };

            await _context.Author.AddAsync(newAuthor);
            var resp = await _context.SaveChangesAsync();

            if (resp > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new Exception("Can´t add a new author");
            }



        }

    }
}
