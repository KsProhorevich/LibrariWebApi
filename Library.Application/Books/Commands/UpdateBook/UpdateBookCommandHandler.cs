using MediatR;
using Microsoft.EntityFrameworkCore;
using Library.Application.Interfaces;
using Library.Domain;
using Library.Application.Common.Exceptions;

namespace Library.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly ILibraryDbContext _dbContext;

        public UpdateBookCommandHandler(ILibraryDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            // Поиск книги по Id
            var entity = await _dbContext.Books
                .FirstOrDefaultAsync(book => book.Id == request.Id, cancellationToken);

            // Проверка на существование книги
            if (entity == null)
            {
                throw new NotFoundException(nameof(Book), request.Id);
            }

            // Обновление полей книги (Id остается прежним)
            entity.Name = request.Name;
            entity.Genre = request.Genre;
            entity.Author = request.Author;
            entity.Description = request.Description;
            entity.BorrowedAt = request.Time; // Обновление времени, когда книгу взяли

            // Сохранение изменений
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value; // Возвращаем результат
        }
    }
}
