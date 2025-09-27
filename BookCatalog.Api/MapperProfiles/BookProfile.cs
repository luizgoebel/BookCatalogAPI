using AutoMapper;
using BookCatalog.Api.DTOs.Book;
using BookCatalog.Model.Entities;

namespace BookCatalog.Api.MapperProfiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<BookCreationDto, Book>();
        CreateMap<BookUpdateDto, Book>();
        CreateMap<Book, BookResponseDto>();
    }
}
