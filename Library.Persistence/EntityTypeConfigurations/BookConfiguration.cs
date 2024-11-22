using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Library.Domain;

namespace Library.Persistence.EntityTypeConfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(book => book.Id);  // Первичный ключ
            builder.HasIndex(book => book.Id).IsUnique();  // Уникальный индекс
            builder.Property(book => book.Name).HasMaxLength(250).IsRequired();  // Название книги
            builder.Property(book => book.Genre).HasMaxLength(100);  // Жанр книги
            builder.Property(book => book.Description).HasMaxLength(1000);  // Описание книги

            
        }
    }
}
