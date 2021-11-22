using AutoMapper;
using BookStore.Books.Api.Application.ModelsDto;
using BookStore.Books.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Books.Api.Test.Helpers
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<Book, BookDto>();
        }
    }
}
