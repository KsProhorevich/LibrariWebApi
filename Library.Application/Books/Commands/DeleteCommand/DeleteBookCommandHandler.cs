using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Library.Application.Interfaces;
using Library.Application.Common.Exceptions;
using Library.Domain;
using Library.Application.Books.Commands.DeleteCommand;

namespace Library.Application.Books.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly ILibraryDbContext _dbContext;

        public DeleteBookCommandHandler(ILibraryDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Books
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Book), request.Id); // Исключение, если книга не найдена
            }

            _dbContext.Books.Remove(entity); // Удаление книги
            await _dbContext.SaveChangesAsync(cancellationToken); // Сохранение изменений

            return Unit.Value; // Возвращаем единицу, указывая на успешное завершение операции
        }
    }
}
