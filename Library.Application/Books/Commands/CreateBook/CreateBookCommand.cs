using System;
using MediatR;

namespace Library.Application.Books.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<Guid>
    {
        public string Name { get; set; } // Название книги
        public string Genre { get; set; } // Жанр книги
        public string Description { get; set; } // Описание книги
        public string Author { get; set; } // Автор книги
        public DateTime? BorrowedAt { get; set; } // Время, когда книгу взяли
        public Guid Id { get; set; }
    }
}
