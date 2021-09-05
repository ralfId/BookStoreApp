using BookStore.Shop.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Shop.Api.Persistence
{
    public class ContextShopping : DbContext
    {
        public ContextShopping(DbContextOptions<ContextShopping> options) : base(options)
        {

        }

        public DbSet<ShoppingSession> ShoppingSession { get; set; }
        public DbSet<ShoppingDetail> ShoppingDetail { get; set; }
    }
}
