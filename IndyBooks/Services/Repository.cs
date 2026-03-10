using IndyBooks.Models;
using IndyBooks.ViewModels;

namespace IndyBooks.Services;

public class Repository
{
    private IndyBooksDataContext _db;

    public Repository(IndyBooksDataContext db)
    {
        _db = db;
    }   
    //Property to return ALL Books on sale (price greater than 90) with the price reduced by 50%
    public decimal Sale{ get; set; } = 0.5m; //percentage off for the sale
    public int SaleLimit { get;set; } = 90; // the item price above this amount will be on sale
       public IEnumerable<Book> SaleResults => _db.Books
        .Where(b => b.Price > SaleLimit)
        .Select(b => new Book
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Price = b.Price * Sale
            }); 
    public IEnumerable<Book> searchResults(SearchVM searchVM) {
            IQueryable<Book> foundBooks = _db.Books; // start with entire collection

            //Filter the collection using the non-empty Title Field as noted
            if (searchVM.Title != null && searchVM.Title.Trim().Length > 0)
            {
                //Filter the collection by Title which "contains" the given string
                foundBooks = foundBooks
                             .Where(b => b.Title.Contains(searchVM.Title));
            }

            //Add similar logic to filter foundbooks collection by last part of the Author's Name, if given
            // (HINT: consider the EndsWith() method, also adjust the Search View and ViewModel to add items)
            if (searchVM.AuthorLastName != null && searchVM.AuthorLastName.Trim().Length > 0)
            {
                foundBooks = foundBooks
                            .Where(b => b.Author.Name.EndsWith(searchVM.AuthorLastName));
            }
            //Adds similar logic to filter foundbooks collection by price, if given
            //       order results by descending price 
            // (Note: you will need to adjust the Search ViewModel and View to add search fields)
            if(searchVM.MinPrice > 0)
            {
                foundBooks = foundBooks
                             .Where(b => b.Price > searchVM.MinPrice);
            }
            if(searchVM.MaxPrice > 0)
            {
                foundBooks = foundBooks
                             .Where(b => b.Price < searchVM.MaxPrice);
            } 



            return foundBooks.ToList();

    }   

};     



