using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Shop.Api.Models
{
    public class ShoppingSession
    {
        [Key]
        public int ShoppingSesionId { get; set; }
        public DateTime? CreationDate { get; set; }
        public ICollection<ShoppingDetail> shoppingDetails { get; set; }
    }
}
