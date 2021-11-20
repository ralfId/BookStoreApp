using BookStore.Shop.Api.Application.ModelsDto;
using BookStore.Shop.Api.Persistence;
using BookStore.Shop.Api.RemoteServices.IServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Shop.Api.Application.ShoppingFeatures.Queries
{
    public class GetShoppingDetailQuery : IRequest<ShoppingCartDto>
    {
        public int ShoppingSessionId { get; set; }
    }

    public class GetShoppingDetailQueryHandler : IRequestHandler<GetShoppingDetailQuery, ShoppingCartDto>
    {
        private readonly ContextShopping _context;
        private readonly IBookServices _bookServices;
        public GetShoppingDetailQueryHandler(ContextShopping context, IBookServices bookServices)
        {
            _context = context;
            _bookServices = bookServices;
        }

        public async Task<ShoppingCartDto> Handle(GetShoppingDetailQuery request, CancellationToken cancellationToken)
        {
            var shoppingSession = await _context.ShoppingSession.FirstOrDefaultAsync(x => x.ShoppingSesionId == request.ShoppingSessionId);
            var shoppingDetail = await _context.ShoppingDetail.Where(x => x.ShoppingSesionId == request.ShoppingSessionId).ToListAsync();

            var listShopCartDto = new List<ShoppingItemDto>();

            foreach (var book in shoppingDetail)
            {
                var getBooks = await _bookServices.GetBookAsync(new Guid(book.SelectedProduct));

                if (getBooks.result)
                {
                    var objBook = getBooks.book;
                    listShopCartDto.Add(new ShoppingItemDto { BookTitle = objBook.Title, Creationdate = objBook.PublicationDate, BookId = objBook.BookId }) ;
                }
            }
            return new ShoppingCartDto
            {
                ShoppingId = shoppingSession.ShoppingSesionId,
                CreationDate = shoppingSession.CreationDate,
                ProductList = listShopCartDto
            };
        }
    }
}
