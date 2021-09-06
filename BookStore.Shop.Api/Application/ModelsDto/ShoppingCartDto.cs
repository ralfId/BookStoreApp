using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Shop.Api.Application.ModelsDto
{
    public class ShoppingCartDto
    {
        public int ShoppingId { get; set; }
        public DateTime? CreationDate { get; set; }
        public List<ShoppingDetailDto> ProductList { get; set; }
    }
}
