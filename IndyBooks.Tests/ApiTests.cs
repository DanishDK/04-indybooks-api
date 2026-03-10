using IndyBooks.Models;
using IndyBooks.Services;
using IndyBooks.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IndyBooks.Tests;
public class ApiWriterTests
{
    private readonly Mock<IWriterService> _mockWriterService;
   private readonly ApiController _controller;

    public ApiWriterTests()
    {
        // Arrange (setup the mock and the controller with the mock object)
        _mockWriterService = new Mock<IWriterService>();
        _controller = new ApiController(_mockWriterService.Object);
    }

    [Fact]
    public void GetWriters_ReturnsOkResultWithListOfWriters()
    {
        // Arrange
        var mockWriters = new List<Writer>
        {
            new Writer { Id = 1, Name = "Maya Angelou" },
            new Writer { Id = 2, Name = "Rupert Sheldrake" }
        };

        // Tell the mock service what to return when ApiController is called
        _mockWriterService.Setup(service => service.GetWriterList())
                           .Returns(mockWriters);

        // Act
        var result =  _controller.GetWriters();

        // Assert
        // Verify that the result is an OK (HTTP 200) status
        var okResult = Assert.IsType<OkObjectResult>(result);

        // Verify that the returned model is the list of products we mocked
        var returnedProducts = Assert.IsType<List<Writer>>(okResult.Value);
        Assert.Equal(2, returnedProducts.Count);
    }
 [Fact]
 public void GetWriterById_ReturnsOKWithWriter()
    {
        // Arrange
        var mockWriters = new List<Writer>
        {
            new Writer { Id = 1, Name = "Maya Angelou" },
            new Writer { Id = 2, Name = "Rupert Sheldrake" }
        };
        long id = 2;

        // Tell the mock service what to return when ApiController is called
        _mockWriterService.Setup(service => service.GetWriterById(id))
                           .Returns(mockWriters.Single(w=>w.Id == id));

        // Act
        var result =  _controller.GetWriter(id);

        // Assert
        // Verify that the result is an OK (HTTP 200) status
        var okResult = Assert.IsType<OkObjectResult>(result);
        
        // Verify that the returned model is mocked writer we selected
        var returnedWriter = Assert.IsType<Writer>(okResult.Value);
        Assert.Equal(mockWriters[2].Name, returnedWriter.Name); //TODO: identify and correct this error
    }
    [Fact]
    public void GetWriterById_ReturnsNotFoundwithoutWriter()
    {
        
        // Arrange
        long nonExistentId = 0;

        // Tell the mock service what to return when ApiController is called
        _mockWriterService.Setup(service => service.GetWriterById(nonExistentId))
                           .Returns((Writer?) null);

        // Act
        //TODO: Nothing here - this tests fails because of TODO in the ApiController
        var result =  _controller.GetWriter(nonExistentId);

        // Assert
        // Verify that the result is an NotFound (HTTP 404) status
        Assert.IsType<NotFoundResult>(result);
        
    }
}