using Microsoft.AspNetCore.Mvc;
using reman.Controllers;
using reman.Models;
using reman.Data;
using Moq;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace reman.Tests;

public class RealEstateControllerTest
{
    [Fact]
    public async Task Index_ReturnsListOfAllRealEstates()
    {
        var data = new List<RealEstate>
        {
            new RealEstate { Id = 1, Name = "Real Estate 1", City = "Paris" }
        };

        var mockSet = new Mock<DbSet<RealEstate>>();

        var contextMock = new Mock<RemanContext>();
        RealEstateController controller = new RealEstateController(contextMock.Object);
        contextMock.Setup(c => c.RealEstates).ReturnsDbSet(data);

        var response = await controller.Index();

        Assert.Equal(response.Value[0].Id, data.ToList()[0].Id);
    }

    [Fact]
    public async Task Create_WhenValidParamsProvided_CreatesNewRealEstate()
    {
        var data = new List<RealEstate>
        {
            new RealEstate { Id = 1, Name = "Real Estate 1" }
        };
        
        var mockSet = new Mock<DbSet<RealEstate>>();
        var contextMock = new Mock<RemanContext>();
        contextMock.Setup(c => c.RealEstates).Returns(mockSet.Object);

        RealEstateController controller = new RealEstateController(contextMock.Object);
        RealEstate realEstate = new RealEstate { Name = "Real Estate 2" };

        var result = await controller.Create(realEstate);

        // Assert
        var actionResult = Assert.IsType<ActionResult<RealEstate>>(result);
        var returnValue = Assert.IsType<RealEstate>(actionResult.Value);
        Assert.Equal(realEstate, returnValue);

        mockSet.Verify(m => m.Add(It.IsAny<RealEstate>()), Times.Once);
        contextMock.Verify(m => m.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        // Assert.Equal(response.Value.Name, "Real Estate 2");
        // mockSet.Verify(m => m.Add(It.IsAny<RealEstate>()), Times.Once);
    }

    [Fact]
    public async Task Create_WhenInvalidParamsProvided_ReturnsValidationErrors()
    {
        var data = new List<RealEstate>
        {
            new RealEstate { Id = 1, Name = "Real Estate 1" }
        };
        
        var mockSet = new Mock<DbSet<RealEstate>>();
        var contextMock = new Mock<RemanContext>();
        contextMock.Setup(c => c.RealEstates).Returns(mockSet.Object);

        RealEstateController controller = new RealEstateController(contextMock.Object);
        controller.ModelState.AddModelError("Name", "Name is required");

        RealEstate realEstate = new RealEstate();
        var result = await controller.Create(realEstate);

        // Assert
        var actionResult = Assert.IsType<ActionResult<RealEstate>>(result);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

        var problemDetails = Assert.IsType<SerializableError>(badRequestResult.Value);
        Assert.Contains("Name", problemDetails);
        Assert.Contains("Name is required", (string[])problemDetails["Name"]);

        mockSet.Verify(m => m.Add(It.IsAny<RealEstate>()), Times.Never);
    }

  [Fact]
    public async Task GetById_ReturnsRealEstateById()
    {
        var data = new List<RealEstate>
        {
        new RealEstate { Id = 1, Name = "Real Estate 1", City = "Paris" }
        };

        var mockSet = new Mock<DbSet<RealEstate>>();
        var contextMock = new Mock<RemanContext>();
        contextMock.Setup(c => c.RealEstates).ReturnsDbSet(data);

        RealEstateController controller = new RealEstateController(contextMock.Object);
        var response = await controller.GetById(1);

        // Assert.Equivalent(1, response.Value.Id);
        // Assert.Equivalent("Real Estate 1", response.Value.Name);
        // Assert.Equivalent("Paris", response.Value.City);
    }

  [Fact]
  public async Task Update_WhenValidParamsProvided_UpdatesRealEstate()
  {
        var options = new DbContextOptionsBuilder<RemanContext>()
                .UseInMemoryDatabase(databaseName: "RealEstateDb")
                .Options;

            using (var context = new RemanContext(options))
            {
                var realEstate = new RealEstate { Id = 1, Name = "Test RealEstate", City = "London", Street = "Street 1", HouseNumber = 1};
                context.RealEstates.Add(realEstate);
                context.SaveChanges();

                var controller = new RealEstateController(context);

                realEstate.Name = "Updated RealEstate";

                // Act
                var result = await controller.Update(1, realEstate);

                // Assert
                var actionResult = Assert.IsType<ActionResult<RealEstate>>(result);
                var returnValue = Assert.IsType<RealEstate>(actionResult.Value);
                Assert.Equal("Updated RealEstate", returnValue.Name);
            }

  }
}