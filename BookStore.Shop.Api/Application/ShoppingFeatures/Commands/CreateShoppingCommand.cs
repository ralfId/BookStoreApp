using BookStore.Shop.Api.Models;
using BookStore.Shop.Api.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Shop.Api.Application.ShoppingFeatures.Commands
{
    public class CreateShoppingCommand : IRequest
    {
        public DateTime CreationDate { get; set; }
        public List<string> ProductsList { get; set; }
    }

    public class CreateShoppingCommandHandler : IRequestHandler<CreateShoppingCommand>
    {
        private readonly ContextShopping _context;

        public CreateShoppingCommandHandler(ContextShopping context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateShoppingCommand request, CancellationToken cancellationToken)
        {

            var session = new ShoppingSession
            {
                CreationDate = request.CreationDate
            };

            _context.ShoppingSession.Add(session);
            var contextResp = await _context.SaveChangesAsync();

            if (contextResp == 0)
            {
                throw new Exception("Can´t create shopping cart");
            }

            int idSession = session.ShoppingSesionId;

            request.ProductsList.ForEach(product =>
            {
                var shoppingDetail = new ShoppingDetail
                {
                    CreationDate = DateTime.UtcNow,
                    ShoppingSesionId = idSession,
                    SelectedProduct = product
                };

                _context.ShoppingDetail.Add(shoppingDetail);
            });

            contextResp = await _context.SaveChangesAsync();

            if (contextResp > 0)
            {
                return Unit.Value;
            }
            else
            {
                throw new Exception("Can´t insert shopping detail");
            }


        }
    }
}
