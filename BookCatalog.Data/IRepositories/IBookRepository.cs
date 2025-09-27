using BookCatalog.Model.Entities;

namespace BookCatalog.Data.IRepositories;

public interface IBookRepository
{
    Task<Book> AddAsync(Book book);
    Task<Book> GetByIdAsync(int id);
}
