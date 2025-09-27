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
        this._context.Set<Book>().Add(book);
        await this._context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> DeleteAsync(int id)
    {
        Book book = await this._context.Set<Book>().FindAsync(id);
        this._context.Set<Book>().Remove(book);
        await this._context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await this._context.Set<Book>().FindAsync(id);
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        this._context.Set<Book>().Update(book);
        await this._context.SaveChangesAsync();
        return book;
    }
}
