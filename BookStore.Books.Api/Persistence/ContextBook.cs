using BookStore.Books.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Books.Api.Persistence
{
    public class ContextBook :DbContext
    {
        public ContextBook(DbContextOptions<ContextBook> options) : base(options)
        {

        }

        public DbSet<Book> Book { get; set; }
    }
}
