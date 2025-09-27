using BookCatalog.Model.Entities;

namespace BookCatalog.Data.IRepositories;

/// <summary>
/// Contrato para operações de persistência da entidade <see cref="Book"/>.
/// Define métodos assíncronos para criação, atualização, consulta e exclusão.
/// </summary>
/// <remarks>
/// Responsabilidades:
/// - Isolar o acesso a dados da camada de domínio/aplicação.
/// - Facilitar testes através de mocking.
/// - Garantir operações básicas (CRUD) sobre livros.
/// </remarks>
public interface IBookRepository
{
    /// <summary>
    /// Adiciona um novo livro ao repositório.
    /// </summary>
    /// <param name="book">Instância do livro a ser persistida. Deve ser válida.</param>
    /// <returns>Livro persistido já com identificador e metadados preenchidos.</returns>
    /// <exception cref="ArgumentNullException">Lançada quando <paramref name="book"/> é nulo.</exception>
    Task<Book> AddAsync(Book book);

    /// <summary>
    /// Atualiza um livro existente.
    /// </summary>
    /// <param name="book">Instância do livro com os dados atualizados. Deve conter um Id válido.</param>
    /// <returns>Livro atualizado após persistência.</returns>
    /// <exception cref="ArgumentNullException">Lançada quando <paramref name="book"/> é nulo.</exception>
    /// <exception cref="KeyNotFoundException">Quando o livro não existe.</exception>
    Task<Book> UpdateAsync(Book book);

    /// <summary>
    /// Obtém um livro pelo identificador.
    /// </summary>
    /// <param name="id">Identificador único do livro.</param>
    /// <returns>Instância do livro encontrado ou null se inexistente.</returns>
    Task<Book> GetByIdAsync(int id);

    /// <summary>
    /// Remove um livro pelo identificador.
    /// </summary>
    /// <param name="id">Identificador do livro a ser removido.</param>
    /// <returns>Livro removido, caso exista; caso contrário pode retornar null.</returns>
    Task<Book> DeleteAsync(int id);
}
