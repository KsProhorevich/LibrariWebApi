using System;
using FluentValidation;

namespace Library.Application.Books.Commands.DeleteCommand
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(deleteBookCommand => deleteBookCommand.Id).NotEqual(Guid.Empty); // Проверка на пустой идентификатор
        }
    }
}
