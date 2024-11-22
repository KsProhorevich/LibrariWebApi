using System;
using Microsoft.EntityFrameworkCore;
using Library.Domain;
using Library.Persistence;

namespace Library.Tests.Common
{
    public class LibraryContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();

        public static LibraryDbContext Create()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new LibraryDbContext(options);
            context.Database.EnsureCreated();
            context.Books.AddRange(
                new Book
                {
                    BorrowedAt = DateTime.Today,
                    Genre = "Horror",
                    Description = "This Description is so long",
                    Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825"),
                    Name = "Title1",
                    Author = "Author1"
                },
               new Book
               {
                   BorrowedAt = DateTime.Today,
                   Genre = "Roman",
                   Description = "This Description is so short",
                   Id = Guid.Parse("8F7CD5A7-3771-4DB9-AF8B-61CAC652296E"),
                   Name = "Title2",
                   Author = "Author2"
               },
                new Book
                {
                    BorrowedAt = DateTime.Now,
                    Genre = "Fantasy",
                    Description = "This Description is so long1",
                    Id = Guid.Parse("B48D297E-E873-4F58-99D3-D4230E04BF98"),
                    Name = "Title4",
                    Author = "Author3"
                },
                new Book
                {
                    BorrowedAt = DateTime.Now,
                    Genre = "Drama",
                    Description = "This Description is so short2",
                    Id = Guid.Parse("962F9C2A-5E4E-48BF-89C1-3D6F4A16804F"),
                    Name = "Title3",
                    Author = "Author4"
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(LibraryDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}