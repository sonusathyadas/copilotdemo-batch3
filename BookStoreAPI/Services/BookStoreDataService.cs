using BookStore.Shared.Entities;
using BookStoreAPI.Data;

namespace BookStoreAPI.Services
{
    /* Add methods for retrieving books, Getting book by id, search books, add new book, update book and delete book data from database using BooStoreDbContext.  #file:'Book.cs' #file:'BookStoreDbContext.cs' 
         * Delete method takes id as argument and search for book, if exits delete the book otherwise throws error. Update method search for book using 
         * id and update if exists otherwise throws error.
        */
    public class BookStoreDataService
    {
        private readonly BookStoreDbContext _dbContext;

        public BookStoreDataService(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _dbContext.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return _dbContext.Books.FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            return _dbContext.Books.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm)).ToList();
        }

        public void AddBook(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }

        //optimize this method
        public void UpdateBook(Book book)
        {
            var existingBook = _dbContext.Books.Find(book.Id);
            if (existingBook == null)
            {
                throw new Exception("Book not found");
            }

            _dbContext.Entry(existingBook).CurrentValues.SetValues(book);
            _dbContext.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book == null)
            {
                throw new Exception("Book not found");
            }

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}
