using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using reman.Controllers;
using reman.Data;
using reman.Models;
using reman.Services;

namespace reman.Tests;

public class EstateUnitTenancyControllerTest
{
    [Fact]
    public async void Index_ReturnsListOfTenanciesForEstateUnit()
    {
        // Given
        var estateUnit = new EstateUnit { Id = 1, Name = "EU00011", Type = EstateUnitType.RESIDENTIAL, RealEstate = new RealEstate { Id = 1, Name = "RE0001" } };
        var tenant = new Tenant { Id = 1, Name = "Tenant" };
        var tenancyData = new List<Tenancy>
        {
            new Tenancy { Id = 1, Tenant = tenant, EstateUnit = estateUnit, StartDate = new DateOnly(2020, 1, 1), EndDate = new DateOnly(2020, 12, 31) },
            new Tenancy { Id = 2, Tenant = tenant, EstateUnit = estateUnit, StartDate = new DateOnly(2021, 1, 1), EndDate = new DateOnly(2021, 12, 31) },
            new Tenancy { Id = 3, Tenant = tenant, EstateUnit = estateUnit, StartDate = new DateOnly(2022, 1, 1), EndDate = new DateOnly(2022, 12, 31) },
        };

        var mockSet = new Mock<DbSet<Tenancy>>();
        var contextMock = new Mock<RemanContext>();
        
        // mockSet.As<IQueryable<Tenancy>>().Setup(m => m.Provider).Returns(tenancyData.Provider);
        // mockSet.As<IQueryable<Tenancy>>().Setup(m => m.Expression).Returns(tenancyData.Expression);
        // mockSet.As<IQueryable<Tenancy>>().Setup(m => m.ElementType).Returns(tenancyData.ElementType);
        mockSet.As<IQueryable<Tenancy>>().Setup(m => m.GetEnumerator()).Returns(tenancyData.GetEnumerator());

        contextMock.Setup(c => c.Tenancies).Returns(mockSet.Object);

        EstateUnitTenancyController controller = new EstateUnitTenancyController(contextMock.Object);
        // When
        var response = await controller.GetTenancies(1);
        // Then
        Assert.Equal(1, response.Value[0].Id);
        Assert.Equal("Tenant", response.Value[0].Tenant.Name);
    }
}