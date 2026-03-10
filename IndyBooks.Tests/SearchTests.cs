using IndyBooks.Services;
using IndyBooks.Models;
using IndyBooks.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace IndyBooks.Tests;

public class SearchTests
{
    private DbContextOptions<IndyBooksDataContext> _dbContextOptions;
    private Repository _repository;

    public SearchTests()
    {
         _dbContextOptions = new DbContextOptionsBuilder<IndyBooksDataContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

        using(var context = new IndyBooksDataContext(_dbContextOptions))
        {
            context.Database.EnsureCreated();
            // Seed the in-memory database with test data
            context.Books.AddRange(
            new Book { Id = 1, Title = "The Great Gatsby", Author = new Writer{ Name = "F. Scott Fitzgerald"}, Price = 16m },
            new Book { Id = 2, Title = "To Know These Days", Author = new Writer{ Name ="Hae Jee"}, Price = 60m },
            new Book { Id = 3, Title = "1984", Author = new Writer{ Name ="George Orwell"}, Price = 96m },
            new Book { Id = 4, Title = "Two Towers", Author = new Writer{ Name ="J.R.R. Tolkien"}, Price = 23m }
            );
            context.SaveChanges();
        }
    }
    [Fact]
    public void SaleResultCorrectlyCalculatesPrice()
    {
        // Arrange
        using var context = new IndyBooksDataContext(_dbContextOptions);
        _repository = new Repository(context);
        var price = 101m;
        var sale = 0.20m;
        _repository.SaleLimit = 100;
        _repository.Sale = sale;

        var book = context.Books.Where(b => b.Id == 4).Single();
        book.Price = price;
        context.Books.Update(book);
        context.SaveChanges();

        // Act
        var results = _repository.SaleResults.ToList();

        // Assert
        Assert.Equal(results[0].Price, price * sale);
    }
    [Fact]
    public void SearchContainsBooksWithTitleContainingSearchTerm()
    {
        // Arrange
        using var context = new IndyBooksDataContext(_dbContextOptions);
        _repository = new Repository(context);
        var searchVM = new SearchVM { Title = "Great" };

        // Act
        var results = _repository.searchResults(searchVM).ToList();

        // Assert
        Assert.Single(results);
        Assert.Equal("The Great Gatsby", results[0].Title);
    }
    
    [Fact]
    public void SearchContainsAllBooksLessThanMaxPrice()
    {
        // Arrange
        using var context = new IndyBooksDataContext(_dbContextOptions);
        _repository = new Repository(context);

        decimal maxPrice = 60m;
        var searchVM = new SearchVM { MaxPrice = maxPrice, MinPrice = 0 };
        var books = context.Books.Where(b=> b.Price < maxPrice);

        // Act
        var results = _repository.searchResults(searchVM).ToList();

        // Assert
        Assert.Equal(books.Count(), results.Count());
    }

    

    
}

