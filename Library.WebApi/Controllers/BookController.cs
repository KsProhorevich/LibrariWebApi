using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Library.Application.Books.Queries.GetBookList;
using Library.Application.Books.Queries.GetBookDetails;
using Library.Application.Books.Commands.CreateBook;
using Library.Application.Books.Commands.UpdateBook;
using Library.Application.Books.Commands.DeleteCommand;
using Library.WebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace Library.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class BookController : BaseController
    {
        private readonly IMapper _mapper;

        public BookController(IMapper mapper) => _mapper = mapper;
        /// <summary>
        /// Gets the list of books
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /book
        /// </remarks>
        /// <returns>Returns BookListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BookListVm>> GetAll()
        {
            var query = new GetBookListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the note by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /book/9D1AA1B1-1CED-457B-9826-38720664C37D
        /// </remarks>
        /// <param name="id">Book id (guid)</param>
        /// <returns>Returns BookDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user in unauthorized</response>

        // Получить детали книги по ID
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BookDetailsVm>> Get(Guid id)
        {
            var query = new GetBookDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the book
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /book
        /// {
        ///     title: "book title",
        ///     details: "book details"
        /// }
        /// </remarks>
        /// <param name="createBookDto">CreateBookDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        // Создать новую книгу
        [HttpPost("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateBookDto createBookDto)
        {
            var command = _mapper.Map<CreateBookCommand>(createBookDto);
            command.Id = UserId;
            var bookId = await Mediator.Send(command);
            return Ok(bookId);
        }
        /// <summary>
        /// Updates the book
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /book
        /// {
        ///     title: "updated book title"
        /// }
        /// </remarks>
        /// <param name="updateBookDto">UpdateBookDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        // Обновить существующую книгу
        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateBookDto updateBookDto)
        {
            var command = _mapper.Map<UpdateBookCommand>(updateBookDto);
            command.Id = UserId;
            await Mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Deletes the book by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /book/88DEB432-062F-43DE-8DCD-8B6EF79073D3
        /// </remarks>
        /// <param name="id">Id of the book (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        // Удалить книгу по ID
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteBookCommand
            {
                Id = id,
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
