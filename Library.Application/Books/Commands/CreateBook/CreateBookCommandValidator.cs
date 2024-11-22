using FluentValidation;

namespace Library.Application.Books.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(createBookCommand =>
                createBookCommand.Name).NotEmpty().MaximumLength(250); // Проверка на пустоту и максимальную длину
            RuleFor(createBookCommand =>
                createBookCommand.Genre).NotEmpty(); // Проверка на пустоту жанра
            RuleFor(createBookCommand =>
                createBookCommand.Description).NotEmpty(); // Проверка на пустоту описания
            RuleFor(createBookCommand =>
                createBookCommand.Author).NotEmpty(); // Проверка на пустоту автора
        }
    }
}
