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
        if (book.PublicationYear > DateTime.Now.ToLocalTime().Year)
        {
            throw new Exception("O ano de publicação não pode ser futuro.");
        }

        return await _repository.AddAsync(book);
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        Book book = await _repository.GetByIdAsync(id) ??
            throw new Exception("Livro não encontrado.");
        return book;
    }
}
