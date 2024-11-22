using MediatR;

namespace Library.Application.Books.Commands.DeleteCommand
{
    public class DeleteBookCommand : IRequest
    {
        public Guid Id { get; set; } // Идентификатор книги для удаления
    }
}
