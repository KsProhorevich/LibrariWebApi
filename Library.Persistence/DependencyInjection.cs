using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Library.Application.Interfaces;

namespace Library.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection
            services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<ILibraryDbContext>(provider =>
                provider.GetService<LibraryDbContext>());
            return services;
        }
    }
}