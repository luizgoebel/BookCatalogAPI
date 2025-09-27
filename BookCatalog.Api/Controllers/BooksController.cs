using AutoMapper;
using BookCatalog.Api.DTOs;
using BookCatalog.Core.IServices;
using BookCatalog.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Api.Controllers;

[Route("api/books")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BooksController(IBookService bookService, IMapper mapper)
    {
        this._bookService = bookService;
        this._mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BookCreationDto dto)
    {
        Book book = _mapper.Map<Book>(dto);
        book.Validar();
        Book createdBook = await _bookService.AddAsync(book);
        BookResponseDto responseDto = _mapper.Map<BookResponseDto>(createdBook);
        return Ok(responseDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Book book = await _bookService.GetByIdAsync(id);
        BookResponseDto responseDto = _mapper.Map<BookResponseDto>(book);
        return Ok(responseDto);
    }
}
