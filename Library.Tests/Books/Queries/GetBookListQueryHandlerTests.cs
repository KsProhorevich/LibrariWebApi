using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Library.Application.Books.Queries.GetBookList;
using Library.Persistence;
using Library.Tests.Common;
using Shouldly;
using Xunit;

namespace Library.Tests.Books.Queries
{
    [Collection("QueryCollection")]
    public class GetBookListQueryHandlerTests
    {
        private readonly LibraryDbContext Context;
        private readonly IMapper Mapper;

        public GetBookListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetBookListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetBookListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetBookListQuery
                {
                    UserId = LibraryContextFactory.UserBId /* Укажите идентификатор пользователя, который вы хотите протестировать */
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BookListVm>();
            result.Books.Count.ShouldBe(4);
        }
    }
}
