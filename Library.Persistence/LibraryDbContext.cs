using Microsoft.EntityFrameworkCore;
using Library.Application.Interfaces;
using Library.Domain;
using Library.Persistence.EntityTypeConfigurations;

namespace Library.Persistence
{
    public class LibraryDbContext : DbContext, ILibraryDbContext
    {
        public DbSet<Book> Books { get; set; }
        
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BookConfiguration());
            base.OnModelCreating(builder);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
