using Microsoft.AspNetCore.Mvc;
using reman.Controllers;
using reman.Models;

namespace reman.Tests;

public class RealEstateControllerTest
{
  [Fact]
  public void Index()
  {
    RealEstateController controller = new RealEstateController();

    List<RealEstate> expected = new List<RealEstate>() { new RealEstate { Id = 1, Name = "Real Estate 1" } };
    
    var response = controller.Index();

    Assert.Equal(response.Value[0].Id, expected[0].Id);
  }
}