using BookStore.Shop.Api.Application.ModelsDto;
using BookStore.Shop.Api.Application.ShoppingFeatures.Commands;
using BookStore.Shop.Api.Application.ShoppingFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreaShopping(CreateShoppingCommand createShopping)
        {
            return await _mediator.Send(createShopping);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCart(int id)
        {
            return await _mediator.Send(new GetShoppingDetailQuery { ShoppingSessionId = id });
        }
    }
}
