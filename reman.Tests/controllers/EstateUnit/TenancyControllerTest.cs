using Moq;
using Moq.EntityFrameworkCore;
using reman.Controllers;
using reman.Data;
using reman.Models;

namespace reman.Tests;

public class EstateUnitTenancyControllerTest
{
    [Fact]
    public async void Index_ReturnsListOfTenanciesForEstateUnit()
    {
        // Given
        var estateUnit = new EstateUnit { Id = 1, Name = "EU00011", Type = EstateUnitType.RESIDENTIAL, RealEstate = new RealEstate { Id = 1, Name = "RE0001" } };
        var data = new List<EstateUnit>
        {
        estateUnit
        };
        var tenant = new Tenant { Id = 1, Name = "Tenant" };
        var tenancies = new List<Tenancy>
        {
        new Tenancy { Id = 1, Tenant = tenant, EstateUnit = estateUnit, StartDate = new DateOnly(2020, 1, 1), EndDate = new DateOnly(2020, 10, 1) }
        };

        var contextMock = new Mock<RemanContext>();
        contextMock.Setup(c => c.Tenancies).ReturnsDbSet(tenancies);
        
        EstateUnitTenancyController controller = new EstateUnitTenancyController(contextMock.Object);
        // When
        var response = await controller.GetTenancies(1);
        // Then
        Assert.Equal(1, response.Value[0].Id);
        Assert.Equal("Tenant", response.Value[0].Tenant.Name);
    }
}