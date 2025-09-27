using BookCatalog.Data.Context;
using BookCatalog.Data.IRepositories;
using BookCatalog.Model.Entities;

namespace BookCatalog.Data.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookContext _context;
    public BookRepository(BookContext context)
    {
        this._context = context;
    }
    public async Task<Book> AddAsync(Book book)
    {
        _context.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _context.FindAsync<Book>(id);
    }
}
