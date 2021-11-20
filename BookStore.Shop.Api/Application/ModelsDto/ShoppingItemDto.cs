using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Shop.Api.Application.ModelsDto
{
    public class ShoppingItemDto
    {
        public Guid? BookId { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public DateTime? Creationdate { get; set; }
    }
}
