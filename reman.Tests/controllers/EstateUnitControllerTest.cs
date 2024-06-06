using Moq;
using reman.Controllers;
using reman.Data;
using reman.Models;
using Moq.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using reman.Dto;

namespace reman.Tests;

public class EstateUnitControllerTest
{
    [Fact]
    public async Task Index_ReturnsListOfAllEstateUnits()
    {
        // Given
        var realEstate1 = new RealEstate(1, "RE0001", "London", "Street 1", 1);
        var realEstate2 = new RealEstate(1, "RE0001", "Paris", "Street 1", 1);
        var data = new List<EstateUnit>
        {
            new EstateUnit { Id = 1, Name = "EU00011", Type = EstateUnitType.RESIDENTIAL, RealEstate = realEstate1 },
            new EstateUnit { Id = 2, Name = "EU00012", Type = EstateUnitType.COMMERCIAL, RealEstate = realEstate2 },
        };

        var contextMock = new Mock<RemanContext>();
        contextMock.Setup(c => c.EstateUnits).ReturnsDbSet(data);

        EstateUnitController controller = new EstateUnitController(contextMock.Object);
        
        // When
        var response = await controller.Index();

        // Then
        Assert.Equal(1, response.Value[0].Id);
        Assert.Equal("EU00011", response.Value[0].Name);
        Assert.Equal(EstateUnitType.RESIDENTIAL, response.Value[0].Type);
        Assert.Equal(1, response.Value[0].RealEstate.Id);
        Assert.Equal("RE0001", response.Value[0].RealEstate.Name);
    }

    [Fact]
    public async Task Post_CreatesNewEstateUnit()
    {
            // Arrange
            var mockContext = new Mock<RemanContext>();
            var realEstate = new RealEstate { Id = 1, Name = "Test Real Estate" };

            mockContext.Setup(m => m.RealEstates.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(realEstate);
            mockContext.Setup(m => m.EstateUnits.Add(It.IsAny<EstateUnit>()));
            mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()))
                .ReturnsAsync(1);

            var controller = new EstateUnitController(mockContext.Object);

            var estateUnit = new EstateUnit { RealEstateId = 1, Name = "Test Unit" };

            // Act
            var result = await controller.Create(estateUnit);

            // Assert
            var actionResult = Assert.IsType<ActionResult<EstateUnitDTO>>(result);
            var createdEstateUnitDTO = Assert.IsType<EstateUnitDTO>(actionResult.Value);
            Assert.Equal(1, createdEstateUnitDTO.RealEstate.Id);
            Assert.Equal("Test Unit", createdEstateUnitDTO.Name);

            mockContext.Verify(m => m.EstateUnits.Add(It.IsAny<EstateUnit>()), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnsEstateUnitById()
    {
        // Given
        var realEstate1 = new RealEstate(1, "RE0001", "London", "Street 1", 1);
        var data = new List<EstateUnit>
        {
            new EstateUnit { Id = 1, Name = "EU00011", Type = EstateUnitType.RESIDENTIAL, RealEstate = realEstate1 },
            new EstateUnit { Id = 2, Name = "EU00012", Type = EstateUnitType.COMMERCIAL, RealEstate = realEstate1 },
        };

        var contextMock = new Mock<RemanContext>();
        contextMock.Setup(c => c.EstateUnits).ReturnsDbSet(data);

        EstateUnitController controller = new EstateUnitController(contextMock.Object);
        
        // When
        var response = await controller.GetById(1);

        // Then
        Assert.Equal(1, response.Value.Id);
        Assert.Equal("EU00011", response.Value.Name);
        Assert.Equal(EstateUnitType.RESIDENTIAL, response.Value.Type);
        Assert.Equal(1, response.Value.RealEstate.Id);
        Assert.Equal("RE0001", response.Value.RealEstate.Name);
    }


    public async Task UpdateById_UpdatesEstateUnit()
    {
        // Given
        var realEstate1 = new RealEstate(1, "RE0001", "London", "Street 1", 1);
        var estateUnit = new EstateUnit {
            Id = 1,
            Name = "EU00011",
            Type = EstateUnitType.RESIDENTIAL,
            RealEstate = realEstate1,
            Area = 54.2f,
            FlatNumber = 1
        };
        var data = new List<EstateUnit>
        {
            new EstateUnit { Id = 1, Name = "EU00011", Type = EstateUnitType.RESIDENTIAL, RealEstate = realEstate1 },
        };

        var contextMock = new Mock<RemanContext>();
        contextMock.Setup(c => c.EstateUnits).ReturnsDbSet(data);

        EstateUnitController controller = new EstateUnitController(contextMock.Object);
        
        // When
        EstateUnitDTO estateUnitDTO = new EstateUnitDTO(estateUnit);
        var response = await controller.UpdateById(1, estateUnit);

        // Then
        Assert.Equal(1, response.Value.Id);
        Assert.Equal("EU00013", response.Value.Name);
        Assert.Equal(EstateUnitType.COMMERCIAL, response.Value.Type);
        Assert.Equal(1, response.Value.RealEstate.Id);
        Assert.Equal("RE0001", response.Value.RealEstate.Name);
    }
}