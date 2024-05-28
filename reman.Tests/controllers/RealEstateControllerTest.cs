using Microsoft.AspNetCore.Mvc;
using reman.Controllers;
using reman.Models;
using reman.Data;
using Moq;
using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;

namespace reman.Tests;

public class RealEstateControllerTest
{
  [Fact]
  public async void Index_ReturnsListOfAllRealEstates()
  {
    var data = new List<RealEstate>
    {
      new RealEstate { Id = 1, Name = "Real Estate 1" }
    };

    var mockSet = new Mock<DbSet<RealEstate>>();

    var contextMock = new Mock<RemanContext>();
    RealEstateController controller = new RealEstateController(contextMock.Object);
    contextMock.Setup(c => c.RealEstates).ReturnsDbSet(data);

    var response = await controller.Index();

    Assert.Equal(response.Value[0].Id, data.ToList()[0].Id);
  }
}