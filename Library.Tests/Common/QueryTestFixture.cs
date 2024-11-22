using AutoMapper;
using System;
using Library.Application.Interfaces;
using Library.Application.Common.Mappings;
using Library.Persistence; // Предположим, что ваш контекст находится в этой папке
using Xunit;

namespace Library.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public LibraryDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = LibraryContextFactory.Create(); // Предположим, что у вас есть аналогичный класс для создания контекста
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(ILibraryDbContext).Assembly)); // Замените на ваш интерфейс
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            LibraryContextFactory.Destroy(Context); // Убедитесь, что у вас есть метод для очистки контекста
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
