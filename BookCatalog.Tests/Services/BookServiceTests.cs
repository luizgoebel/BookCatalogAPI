using BookCatalog.Core.Exceptions;
using BookCatalog.Core.Services;
using BookCatalog.Data.IRepositories;
using BookCatalog.Model.Entities;
using BookCatalog.Model.Exceptions;
using Moq;

namespace BookCatalog.Tests.Services;

public class BookServiceTests
{
    #region Casos de sucesso
    [Fact]
    public async Task AddAsync_ValidBook_ReturnsBook()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var validBook = new Book { Title = "Valid Book", Author = "Autor ok", PublicationYear = 2023 };

        mockRepo.Setup(r => r.AddAsync(validBook)).ReturnsAsync(validBook);

        var service = new BookService(mockRepo.Object);

        // Act
        var result = await service.AddAsync(validBook);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Valid Book", result.Title);
        mockRepo.Verify(r => r.AddAsync(validBook), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidBook_ReturnsBook()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var validBook = new Book { Id = 1, Title = "Valid Book", Author = "Autor ok", PublicationYear = 2023 };

        mockRepo.Setup(r => r.UpdateAsync(validBook)).ReturnsAsync(validBook);

        var service = new BookService(mockRepo.Object);

        // Act
        var result = await service.UpdateAsync(validBook);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Valid Book", result.Title);
        mockRepo.Verify(r => r.UpdateAsync(validBook), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsBook()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var existingId = 10;
        var book = new Book { Id = existingId, Title = "Encontrado", Author = "Autor", PublicationYear = 2022 };

        mockRepo.Setup(r => r.GetByIdAsync(existingId)).ReturnsAsync(book);

        var service = new BookService(mockRepo.Object);

        // Act
        var result = await service.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingId, result.Id);
        Assert.Equal("Encontrado", result.Title);
        mockRepo.Verify(r => r.GetByIdAsync(existingId), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ExistingBook_ReturnsBook()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var existingId = 10;
        var book = new Book { Id = existingId, Title = "Encontrado", Author = "Autor", PublicationYear = 2022 };

        mockRepo.Setup(r => r.DeleteAsync(existingId)).ReturnsAsync(book);
        mockRepo.Setup(r => r.GetByIdAsync(existingId)).ReturnsAsync(book);

        var service = new BookService(mockRepo.Object);

        // Act
        var result = await service.DeleteAsync(existingId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingId, result.Id);
        Assert.Equal("Encontrado", result.Title);
        mockRepo.Verify(r => r.DeleteAsync(existingId), Times.Once);
    }
    #endregion

    #region Casos de falha
    [Fact]
    public async Task GetByIdAsync_InvalidId_ThrowsServiceException()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var service = new BookService(mockRepo.Object);
        var invalidId = 0;

        // Act & Assert
        await Assert.ThrowsAsync<ServiceException>(() => service.GetByIdAsync(invalidId));

        mockRepo.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ThrowsServiceException()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var service = new BookService(mockRepo.Object);
        var nonExistingId = 999;

        mockRepo.Setup(r => r.GetByIdAsync(nonExistingId)).ReturnsAsync((Book)null!);

        // Act & Assert
        await Assert.ThrowsAsync<ServiceException>(() => service.GetByIdAsync(nonExistingId));

        mockRepo.Verify(r => r.GetByIdAsync(nonExistingId), Times.Once);
    }

    [Fact]
    public async Task AddAsync_FutureYear_ThrowsDomainException()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var service = new BookService(mockRepo.Object);
        var futureBook = new Book { Author = "Autor", Title = "Time Traveler", PublicationYear = 2050 };

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => service.AddAsync(futureBook));

        mockRepo.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task AddAsync_AuthorEmpty_ThrowsDomainException()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var service = new BookService(mockRepo.Object);
        var futureBook = new Book { Author = "", Title = "Time Traveler", PublicationYear = 2050 };

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => service.AddAsync(futureBook));

        mockRepo.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task AddAsync_TitleEmpty_ThrowsDomainException()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var service = new BookService(mockRepo.Object);
        var futureBook = new Book { Author = "Autor", Title = "", PublicationYear = 2050 };

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => service.AddAsync(futureBook));

        mockRepo.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_InvalidId_ThrowsServiceException()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var service = new BookService(mockRepo.Object);
        var existingId = 0;

        // Act & Assert
        await Assert.ThrowsAsync<ServiceException>(() => service.DeleteAsync(existingId));

        mockRepo.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingId_ThrowsServiceException()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var service = new BookService(mockRepo.Object);
        var existingId = 999;

        // Act & Assert
        await Assert.ThrowsAsync<ServiceException>(() => service.DeleteAsync(existingId));

        mockRepo.Verify(r => r.AddAsync(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_BookNull_ThrowsDomainException()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var service = new BookService(mockRepo.Object);
        Book book = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAsync(book));

        mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Book>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingId_ThrowsDomainException()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var service = new BookService(mockRepo.Object);
        var nonExistingId = 0;
        var book = new Book { Id = nonExistingId, Author = "Autor", Title = "Titulo", PublicationYear = 2020 };

        // Act & Assert
        await Assert.ThrowsAsync<ServiceException>(() => service.UpdateAsync(book));

        mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Book>()), Times.Never);
    }
    #endregion
}
