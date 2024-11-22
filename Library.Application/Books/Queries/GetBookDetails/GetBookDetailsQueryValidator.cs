using System;
using FluentValidation;

namespace Library.Application.Books.Queries.GetBookDetails
{
    public class GetBookDetailsQueryValidator : AbstractValidator<GetBookDetailsQuery>
    {
        public GetBookDetailsQueryValidator()
        {
            RuleFor(book => book.Id).NotEqual(Guid.Empty);
        }
    }
}
