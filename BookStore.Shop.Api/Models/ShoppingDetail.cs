using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Shop.Api.Models
{
    public class ShoppingDetail
    {
        [Key]
        public int ShoppingSessionId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string SelectedProduct { get; set; }
        public int ShoppingSesionId { get; set; }
        public ShoppingSession shoppingSession { get; set; }
    }
}
