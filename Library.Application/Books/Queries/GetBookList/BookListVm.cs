using System.Collections.Generic;

namespace Library.Application.Books.Queries.GetBookList
{
    public class BookListVm
    {
        public IList<BookLookupDto> Books { get; set; } // Изменено на Books
    }
}
