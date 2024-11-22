using System;
using MediatR;

namespace Library.Application.Books.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest
    {
        public Guid Id { get; set; }       // Идентификатор книги, которую нужно обновить
        public string Name { get; set; }    // Название книги
        public string Genre { get; set; }   // Жанр книги
        public string Description { get; set; } // Описание книги
        public string Author { get; set; }  // Идентификатор автора книги
        public DateTime? Time { get; set; } // Время, когда книгу взяли
    }
}
