using Moq;
using reman.Controllers;
using reman.Data;
using reman.Models;
using Moq.EntityFrameworkCore;

namespace reman.Tests;

public class EstateUnitControllerTest
{
  [Fact]
  public async Task Index_ReturnsListOfAllEstateUnits()
  {
    // Given
    var data = new List<EstateUnit>
    {
      new EstateUnit { Id = 1, Name = "EU00011", Type = EstateUnitType.RESIDENTIAL },
      new EstateUnit { Id = 1, Name = "EU00012", Type = EstateUnitType.COMMERCIAL },
    };

    var contextMock = new Mock<RemanContext>();
    contextMock.Setup(c => c.EstateUnits).ReturnsDbSet(data);

    EstateUnitController controller = new EstateUnitController(contextMock.Object);
    
    // When
    var response = await controller.Index();

    // Then
    Assert.Equal(response.Value[0].Id, data.ToList()[0].Id);
  }
}