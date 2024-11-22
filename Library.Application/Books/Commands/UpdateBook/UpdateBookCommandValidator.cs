using FluentValidation;

namespace Library.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(updateBookCommand => updateBookCommand.Id).NotEqual(Guid.Empty); // Проверка, что Id не пустой
            RuleFor(updateBookCommand => updateBookCommand.Name)
                .NotEmpty().MaximumLength(250);
            RuleFor(updateBookCommand => updateBookCommand.Genre)
                .NotEmpty().MaximumLength(100); // Проверка на жанр
            RuleFor(updateBookCommand => updateBookCommand.Description)
                .MaximumLength(1000); // Проверка на описание
        }
    }
}
