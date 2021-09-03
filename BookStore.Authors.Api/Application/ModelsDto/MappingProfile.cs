using AutoMapper;
using BookStore.Authors.Api.Models;

namespace BookStore.Authors.Api.Application.ModelsDto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>();
        }
    }
}
