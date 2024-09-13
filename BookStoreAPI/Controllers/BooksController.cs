﻿using BookStore.Shared.Entities;
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



    }
}
