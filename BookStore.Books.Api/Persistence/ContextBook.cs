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
        public ContextBook()
        {

        }

        public ContextBook(DbContextOptions<ContextBook> options) : base(options)
        {

        }

        public virtual DbSet<Book> Book { get; set; }
    }
}
