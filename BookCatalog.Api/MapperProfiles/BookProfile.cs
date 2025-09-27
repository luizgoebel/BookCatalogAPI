using AutoMapper;
using BookCatalog.Api.DTOs;
using BookCatalog.Model.Entities;

namespace BookCatalog.Api.MapperProfiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookCreationDto, Book>();
        CreateMap<Book, BookResponseDto>();
    }
}
