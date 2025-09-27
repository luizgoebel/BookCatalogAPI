using BookCatalog.Model.Entities;

namespace BookCatalog.Core.IServices;

public interface IBookService
{
    Task<Book> AddAsync(Book book);
    Task<Book> GetByIdAsync(int id);
}
