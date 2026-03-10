using IndyBooks.Services;
using IndyBooks.Models;
using IndyBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace IndyBooks.Controllers;

    public class AdminController : Controller
    {
        private IndyBooksDataContext _db;
        private Repository _repo;
        public AdminController(IndyBooksDataContext db, Repository repo) 
        { 
            _db = db; 
            _repo = repo;
        }

        /***
         * READ       
         */
        [HttpGet]
        public IActionResult Index(long id)
        {
            // If passed an id > 0 (this is its Route Parameter),
            //  - Filter books by the id, 
            //  - Otherwise return the entire collection of Books, ordered by SKU.
            IEnumerable<Book> books;
            if (id > 0) {
                books = _db.Books.Include(b => b.Author)
                     .Where(b => b.Id == id);
            }
            else {
               books = _db.Books.Include(b => b.Author)
                    .OrderBy(b => b.SKU);
            }
            var searchResults = new SearchResultsVM
            {
                Books = books,
                IsSale = false //Just display the regular prices
            };
            return View("SearchResults", searchResults);
        }
        /***
         * DELETE
         */
        [HttpGet]
        public IActionResult RemoveBook(long id)
        {
            //TODO: Remove the Book associated with the given id number; Save Changes
            _db.Books.Remove(new Book{Id = id});
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        /***
         * CREATE
         */
        [HttpGet]
        public IActionResult CreateBook()
        {
            //TODO: Build a new CreateBookViewModel with a complete set of Writers from the database
            //      sort the Writers by Name
            CreateBookVM bookVM = new CreateBookVM
            {
                Authors = _db.Writers
            };

            return View(bookVM); //TODO: pass the ViewModel to populate the "AUTHOR NAME" drop down in the CreateBook View
        }
        [HttpPost]
        public IActionResult CreateBook(CreateBookVM bookVM, long id)
        {
            Writer writer;
            //TODO: Build the Writer object using the view Model info
            writer = String.IsNullOrEmpty(bookVM.Name) ? _db.Writers.SingleOrDefault(w => w.Id == bookVM.AuthorId) :
                                                        new Writer { Name = bookVM.Name };


            //Build the Book using the parameter data and your newly created author.
            Book book = new Book
            {
                Title = bookVM.Title,
                SKU = bookVM.SKU,
                Price = bookVM.Price,
                Author = writer,
                Id = id
            };


            //TODO: Add author and book to their DbSets; SaveChanges
            if (bookVM.Name!= null) _db.Add(writer);
            if (id == 0) _db.Add(book);
            else _db.Update(book);
            _db.SaveChanges();

            //TODO: Show the book by passing the Book's id (rather than 1) to the Index Action 
            return RedirectToAction("Index", new { id = book.Id });
        }


        /***
         *  UPDATE a Book (reusing the CreateBook View  to ) 
         */
         //TODO: Write a method to take a book id, and load book and author info
         //      into the ViewModel for the CreateBook View
         [HttpGet]
         public IActionResult UpdateBook(long id)
        {
            var book = _db.Books.Include(b => b.Author).Single(b => b.Id == id);
            var bookVM = new CreateBookVM
            {
                BookId = book.Id,
                AuthorId = book.Author.Id,
                Price = book.Price,
                SKU = book.SKU,
                Title = book.Title,
                Authors = _db.Writers
            };
            return View("CreateBook", bookVM);
            
        }
        
        [HttpGet]
        public IActionResult Search() { return View(); }
        [HttpPost]
        public IActionResult Search(SearchVM searchVM)
        {
            SearchResultsVM searchResults = new SearchResultsVM { 
                Books = _repo.SaleResults,
                IsSale = searchVM.HalfPriceSale
            };

            return View("SearchResults", searchResults);

    }
}
