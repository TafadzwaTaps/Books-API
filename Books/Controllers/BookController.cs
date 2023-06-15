using Books.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Books.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public static List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            Book book1 = new Book(1, "The Great Escape", "F. Scott ", 150, new List<string> { "Fiction", "Classic" });
            Book book2 = new Book(2, "How To Kill A Hummingbird", "Harper Lee", 325, new List<string> { "Fiction", "Classic" });
            Book book3 = new Book(3, "Harry Potter and the deathly hallows", "J.K. Rowling", 336, new List<string> { "Fiction", "Fantasy" });

            books.Add(book1);
            books.Add(book2);
            books.Add(book3);
            return books;

        }

        [HttpGet]
        [Route("Book/{id}")]
        public IActionResult Info(int id)
        {
            Book book = GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        public Book GetBookById(int id)
        {
            List<Book> books = GetBooks();
            Book book = books.Find(b => b.Id == id);
            return book;
        }

        public IActionResult List()
        {
            List<Book> books = GetBooks();

            return View(books);
        }

        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [Route("Book/{id}")]
        public ActionResult AddBook(Book book)
        {

            if (ModelState.IsValid)
            {
                return UnprocessableEntity();
            }

            return Ok(book);
        }

        [HttpPut]
        [Route("Book/{id}")]
        public ActionResult EditBook(int id, Book updatedBook)
        {
            List<Book> books = GetBooks();
            Book bookToUpdate = books.FirstOrDefault(b => b.Id == id);

            if (bookToUpdate == null)
            {
                return NotFound(); 
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity();
            }

            bookToUpdate.Title = updatedBook.Title;
            bookToUpdate.Author = updatedBook.Author;
            bookToUpdate.NumberOfPages = updatedBook.NumberOfPages;
            bookToUpdate.Categories = updatedBook.Categories;

            return Ok(bookToUpdate);
        }

        public ActionResult Delete(int id)
        {
            // Delete the book with the specified id from the list of books
            List<Book> books = GetBooks();
            Book book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound(); // Return a 404 Not Found response if the book is not found
            }

            books.Remove(book);

            return Ok(); // Return a 200 OK response
        }

    }
}
