using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Library.Application.Interfaces;

namespace Library.Application.Books.Queries.GetBookList
{
    public class GetBookListQueryHandler
        : IRequestHandler<GetBookListQuery, BookListVm>
    {
        private readonly ILibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBookListQueryHandler(ILibraryDbContext dbContext,
            IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<BookListVm> Handle(GetBookListQuery request,
            CancellationToken cancellationToken)
        {
            // Получаем IQueryable<Book> и применяем ProjectTo
            var bookQuery = _dbContext.Books
                .ProjectTo<BookLookupDto>(_mapper.ConfigurationProvider);

            var books = await bookQuery.ToListAsync(cancellationToken); // Применяем ToListAsync к IQueryable

            return new BookListVm { Books = books }; // Изменено на Books
        }
    }
}
