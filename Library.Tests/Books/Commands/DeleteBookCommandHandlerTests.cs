using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Application.Common.Exceptions;
using Library.Application.Books.Commands.DeleteCommand;
using Library.Application.Books.Commands.CreateBook;
using Library.Tests.Common;
using Xunit;
using Library.Application.Books.Commands.DeleteBook;

namespace Library.Tests.Books.Commands
{
    public class DeleteBookCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteBookCommandHandler_Success()
        {
            // Arrange
            var createHandler = new CreateBookCommandHandler(Context);
            var bookId = await createHandler.Handle(new CreateBookCommand
            {
                Name = "Test Book",
                Description = "This is a test book.",
                Genre = "Fiction",
                BorrowedAt = null,
                Author = "Author 1"
            }, CancellationToken.None);

            var deleteHandler = new DeleteBookCommandHandler(Context);

            // Act
            await deleteHandler.Handle(new DeleteBookCommand { Id = bookId }, CancellationToken.None);

            // Assert
            Assert.Null(Context.Books.SingleOrDefault(book => book.Id == bookId)); // Проверяем, что книга удалена
        }

        [Fact]
        public async Task DeleteBookCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteBookCommandHandler(Context);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new DeleteBookCommand { Id = Guid.NewGuid() }, CancellationToken.None));
        }
    }
}
