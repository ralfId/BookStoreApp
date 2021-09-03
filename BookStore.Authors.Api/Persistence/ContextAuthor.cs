using BookStore.Authors.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Authors.Api.Persistence
{
    public class ContextAuthor : DbContext
    {
        public ContextAuthor(DbContextOptions<ContextAuthor> options) : base(options) { }

        public DbSet<Author> Author{ get; set; }
        public DbSet<Academic> Academic { get; set; }
    }
}
