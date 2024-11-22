using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Application.Common.Exceptions;
using Library.Application.Books.Commands.UpdateBook;
using Library.Tests.Common;
using Xunit;
using Library.Application.Books.Commands.CreateBook;

namespace Library.Tests.Books.Commands
{
    public class UpdateBookCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateBookCommandHandler_Success()
        {
            // Arrange
            var createHandler = new CreateBookCommandHandler(Context);
            var bookId = await createHandler.Handle(new CreateBookCommand
            {
                Name = "Test Book",
                Description = "This is a test book.",
                Genre = "Fiction",
                Author = "Author1",
                BorrowedAt = null
            }, CancellationToken.None);

            var updateHandler = new UpdateBookCommandHandler(Context);

            // Act
            await updateHandler.Handle(new UpdateBookCommand
            {
                Id = bookId,
                Name = "Updated Test Book",
                Genre = "Updated Genre",
                Description = "Updated Description",
                Author = "Updated Author",
                Time = DateTime.Now // Укажите нужное время
            }, CancellationToken.None);

            // Assert
            var updatedBook = await Context.Books.FindAsync(bookId);
            Assert.NotNull(updatedBook);
            Assert.Equal("Updated Test Book", updatedBook.Name);
            Assert.Equal("Updated Genre", updatedBook.Genre);
            Assert.Equal("Updated Description", updatedBook.Description);
            Assert.Equal("Updated Author", updatedBook.Author);
        }

        [Fact]
        public async Task UpdateBookCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdateBookCommandHandler(Context);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new UpdateBookCommand
                {
                    Id = Guid.NewGuid(), // Используем несуществующий идентификатор
                    Name = "Test Book",
                    Genre = "Fiction",
                    Description = "This is a test book.",
                    Author = "New Author",
                }, CancellationToken.None));
        }
    }
}
