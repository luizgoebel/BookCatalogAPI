using BookCatalog.Model.Exceptions;

namespace BookCatalog.Model.Entities;

public class Book : BaseModel<Book>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublicationYear { get; set; }

    public bool Validar()
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new DomainException("Por favor, preencher o titulo.");

        if (string.IsNullOrWhiteSpace(Author))
            throw new DomainException("Por favor, preencher o autor.");

        var anoAtual = DateTime.Now.ToLocalTime().Year;
        if (PublicationYear < 1 || PublicationYear > anoAtual)
            throw new DomainException("Por favor, verificar a data de publicação informada.");
        
        return true;
    }

    public void Alterar(Book book)
    {
        this.Title = book.Title;
        this.Author = book.Author;
        this.PublicationYear = book.PublicationYear;
        this.Validar();
    }
}
