using Microsoft.AspNetCore.Mvc;
using reman.Controllers;

namespace reman.Tests;

public class HomeControllerTest
{
  [Fact]
  public void Index()
  {
    HomeController controller = new HomeController();

    string actual = "Hello";
    string expected = controller.Index();

    Assert.Equal(expected, actual);
  }
}