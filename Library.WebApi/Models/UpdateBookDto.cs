using AutoMapper;
using Library.Application.Common.Mappings;
using Library.Application.Books.Commands.UpdateBook;

namespace Library.WebApi.Models
{
    public class UpdateBookDto : IMapWith<UpdateBookCommand>
    {
        public Guid Id { get; set; }       // Идентификатор книги, которую нужно обновить
        public string Name { get; set; }    // Название книги
        public string Genre { get; set; }   // Жанр книги
        public string Description { get; set; } // Описание книги
       
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateBookDto, UpdateBookCommand>()
                .ForMember(noteCommand => noteCommand.Id,
                    opt => opt.MapFrom(noteDto => noteDto.Id))
                .ForMember(noteCommand => noteCommand.Name,
                    opt => opt.MapFrom(noteDto => noteDto.Name))
                .ForMember(noteCommand => noteCommand.Genre,
                    opt => opt.MapFrom(noteDto => noteDto.Genre))
              .ForMember(noteCommand => noteCommand.Description,
                    opt => opt.MapFrom(noteDto => noteDto.Description));
        }
    }
}