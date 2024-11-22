using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Application.Books.Queries.GetBookDetails;
using Library.Persistence;
using Library.Tests.Common;
using Shouldly;
using Xunit;

namespace Library.Tests.Books.Queries
{
    [Collection("QueryCollection")]
    public class GetBookDetailsQueryHandlerTests
    {
        private readonly LibraryDbContext Context;
        private readonly IMapper Mapper;

        public GetBookDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetBookDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetBookDetailsQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetBookDetailsQuery
                {
                    Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825") 
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BookDetailsVm>();
            result.Name.ShouldBe("Title1");
            result.BorrowedAt.ShouldBe(DateTime.Today);
        }
    }
}
