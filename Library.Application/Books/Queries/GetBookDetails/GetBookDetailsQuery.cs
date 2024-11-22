using System;
using MediatR;

namespace Library.Application.Books.Queries.GetBookDetails
{
    public class GetBookDetailsQuery : IRequest<BookDetailsVm>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
