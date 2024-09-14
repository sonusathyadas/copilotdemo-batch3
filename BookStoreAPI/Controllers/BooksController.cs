using BookStore.Shared.Entities;
using BookStoreAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDataService _bookStoreDataService;

        public BooksController(BookStoreDataService bookStoreDataService)
        {
            _bookStoreDataService = bookStoreDataService;
        }

        /// <summary>
        /// Retrieves all books.
        /// </summary>
        /// <returns>An enumerable collection of books.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Book> GetAllBooks()
        {
            return _bookStoreDataService.GetBooks();
        }

        /// <summary>
        /// Retrieves a book by its id.
        /// </summary>
        /// <param name="id">The id of the book.</param>
        /// <returns>The book with the specified id.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Book> GetBookById(int id)
        {
            try
            {
                var book = _bookStoreDataService.GetBookById(id);
                return book;
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Inserts a new book.
        /// </summary>
        /// <param name="book">The book to insert.</param>
        /// <returns>The inserted book.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Book> InsertBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bookStoreDataService.AddBook(book);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        /// <summary>
        /// Updates a book by its id.
        /// </summary>
        /// <param name="id">The id of the book.</param>
        /// <param name="book">The updated book.</param>
        /// <returns>No content if successful, or not found if the book does not exist.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateBook(int id, Book book)
        {
            try
            {
                book.Id = id;
                _bookStoreDataService.UpdateBook(book);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        
        
        /// <summary>
        /// Deletes a book by its id.
        /// </summary>
        /// <param name="id">The id of the book to delete.</param>
        /// <returns>No content if successful, or not found if the book does not exist.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _bookStoreDataService.DeleteBook(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Searches for books based on the provided search text.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns>An enumerable collection of books matching the search text.</returns>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<Book> SearchBooks(string searchText)
        {
            return _bookStoreDataService.SearchBooks(searchText);
        }



        [HttpGet("authors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> GetAuthors()
        {
            var authors = _bookStoreDataService.GetBooks().Select(book => book.Author).Distinct();
            return authors;
        }



    }
}
