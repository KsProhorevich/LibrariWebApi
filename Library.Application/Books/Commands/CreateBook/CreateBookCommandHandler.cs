using MediatR;
using Library.Application.Interfaces;
using Library.Domain;

namespace Library.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly ILibraryDbContext _dbContext;

        public CreateBookCommandHandler(ILibraryDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Id = Guid.NewGuid(), // Генерация нового уникального идентификатора для книги
                Name = request.Name, // Название книги
                Genre = request.Genre, // Жанр книги
                Description = request.Description, // Описание книги
                Author = request.Author, // Автор книги
                BorrowedAt = request.BorrowedAt // Дата, когда книга была взята, если применимо
            };

            // Добавление новой книги в базу данных
            await _dbContext.Books.AddAsync(book, cancellationToken);
            // Сохранение изменений в базе данных
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Возвращение уникального идентификатора созданной книги
            return book.Id;
        }
    }
}
