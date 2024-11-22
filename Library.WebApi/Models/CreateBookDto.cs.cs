using AutoMapper;
using Library.Application.Books.Commands.CreateBook;
using Library.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;
namespace Library.WebApi.Models
{
    public class CreateBookDto : IMapWith<CreateBookCommand>
    {
        [Required]
        public string Name { get; set; } // Название книги
        public Guid AuthorId { get; set; }// Автор книги
        public string Description { get; set; }// Описание книги
        public string Genre { get; set; }// Жанр книги

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateBookDto, CreateBookCommand>()
                .ForMember(bookCommand => bookCommand.Name,
                    opt => opt.MapFrom(bookDto => bookDto.Name))
                .ForMember(bookCommand => bookCommand.Genre,
                    opt => opt.MapFrom(bookDto => bookDto.Genre))
                .ForMember(bookCommand => bookCommand.Description,
                    opt => opt.MapFrom(bookDto => bookDto.Description));
        }
    }
}
