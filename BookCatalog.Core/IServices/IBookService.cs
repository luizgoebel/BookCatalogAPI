using BookCatalog.Model.Entities;

namespace BookCatalog.Core.IServices;

/// <summary>
/// Serviço de domínio responsável por operações de criação, leitura, atualização e exclusão de livros.
/// </summary>
/// <remarks>
/// Implementações devem garantir:
/// - Validação do objeto <see cref="Book"/> antes de persistir.
/// - Lançar exceções apropriadas (ex.: <see cref="ArgumentException"/>, <see cref="KeyNotFoundException"/>).
/// - Atualização dos metadados de auditoria conforme necessário.
/// </remarks>
public interface IBookService
{
    /// <summary>
    /// Atualiza um livro existente.
    /// </summary>
    /// <param name="book">Instância contendo o Id do livro a ser atualizado e os novos dados.</param>
    /// <returns>Livro atualizado após persistência.</returns>
    /// <exception cref="ArgumentNullException">Se <paramref name="book"/> for nulo.</exception>
    /// <exception cref="ArgumentException">Se o Id for inválido ou a validação de domínio falhar.</exception>
    /// <exception cref="KeyNotFoundException">Se o livro não for encontrado.</exception>
    Task<Book> UpdateAsync(Book book);

    /// <summary>
    /// Adiciona um novo livro ao catálogo.
    /// </summary>
    /// <param name="book">Instância do livro a ser criado (Id deve estar ausente ou ignorado).</param>
    /// <returns>Livro persistido com Id gerado.</returns>
    /// <exception cref="ArgumentNullException">Se <paramref name="book"/> for nulo.</exception>
    /// <exception cref="ArgumentException">Se a validação de domínio falhar.</exception>
    Task<Book> AddAsync(Book book);

    /// <summary>
    /// Obtém um livro pelo identificador.
    /// </summary>
    /// <param name="id">Identificador único do livro.</param>
    /// <returns>Livro correspondente ao Id.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Se <paramref name="id"/> for menor ou igual a zero.</exception>
    /// <exception cref="KeyNotFoundException">Se o livro não for encontrado.</exception>
    Task<Book> GetByIdAsync(int id);

    /// <summary>
    /// Remove um livro do catálogo.
    /// </summary>
    /// <param name="id">Identificador único do livro a remover.</param>
    /// <returns>Livro removido (pode ser usado para auditoria/log).</returns>
    /// <exception cref="ArgumentOutOfRangeException">Se <paramref name="id"/> for menor ou igual a zero.</exception>
    /// <exception cref="KeyNotFoundException">Se o livro não for encontrado.</exception>
    Task<Book> DeleteAsync(int id);
}
