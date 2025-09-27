using BookCatalog.Core.Exceptions;
using BookCatalog.Core.IServices;
using BookCatalog.Data.IRepositories;
using BookCatalog.Model.Entities;

namespace BookCatalog.Core.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(IBookRepository repository)
    {
        this._repository = repository;
    }

    public async Task<Book> AddAsync(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);
        if (book.PublicationYear > DateTime.Now.ToLocalTime().Year)
            throw new ServiceException("O ano de publicação não pode ser futuro.");
        return await _repository.AddAsync(book);
    }

    public async Task<Book> DeleteAsync(int id)
    {
        if (id <= 0)
            throw new ServiceException("Livro não encontrado.");
        Book book = await _repository.DeleteAsync(id);
        return book;
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        Book book = await _repository.GetByIdAsync(id) ??
            throw new ServiceException("Livro não encontrado.");
        return book;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        ArgumentNullException.ThrowIfNull(book);
        if (book.Id <= 0)
            throw new ServiceException("Livro não encontrado.");
        book.Alterar(book);
        return await _repository.UpdateAsync(book);
    }
}
