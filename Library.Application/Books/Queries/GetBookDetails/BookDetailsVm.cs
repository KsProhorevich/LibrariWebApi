using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Domain;

namespace Library.Application.Books.Queries.GetBookDetails
{
    public class BookDetailsVm : IMapWith<Book>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime? BorrowedAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, BookDetailsVm>()
                .ForMember(bookVm => bookVm.Name, opt => opt.MapFrom(book => book.Name))
                .ForMember(bookVm => bookVm.Genre, opt => opt.MapFrom(book => book.Genre))
                .ForMember(bookVm => bookVm.Description, opt => opt.MapFrom(book => book.Description))
                .ForMember(bookVm => bookVm.Id, opt => opt.MapFrom(book => book.Id))
                .ForMember(bookVm => bookVm.BorrowedAt, opt => opt.MapFrom(book => book.BorrowedAt));
        }
    }
}
