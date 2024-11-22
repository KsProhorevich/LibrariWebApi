using System.Threading;
using System.Threading.Tasks;
using Library.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Library.Application.Books.Commands.CreateBook;
using Xunit;

namespace Library.Tests.Books.Commands
{
    public class CreateBookCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateBookCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateBookCommandHandler(Context);
            var bookName = "Book Title";
            var bookGenre = "Fiction";
            var bookDescription = "This is a sample book description.";
            var authorName = "Author Name"; // Добавляем автора, если требуется

            // Act
            var bookId = await handler.Handle(
                new CreateBookCommand
                {
                    Name = bookName,
                    Genre = bookGenre,
                    Description = bookDescription,
                    Author = authorName, // Передаем имя автора
                    BorrowedAt = null // Или укажите дату, если требуется
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Books.SingleOrDefaultAsync(book =>
                    book.Id == bookId &&
                    book.Name == bookName &&
                    book.Genre == bookGenre &&
                    book.Description == bookDescription &&
                    book.Author == authorName)); // Проверяем также автора
        }
    }
}
