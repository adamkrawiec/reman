using Moq;
using Moq.EntityFrameworkCore;
using reman.Controllers;
using reman.Data;
using reman.Models;

namespace reman.Tests;

public class TenantControllerTest
{
    [Fact]
    public async Task Index_ReturnsListOfAllTenants()
    {
        // Given
        var data = new List<Tenant>
        {
            new Tenant { Id = 1, Name = "Tenant 1" },
            new Tenant { Id = 2, Name = "Tenant 2" },
        };

        var contextMock = new Mock<RemanContext>();
        contextMock.Setup(c => c.Tenants).ReturnsDbSet(data);

        TenantController controller = new TenantController(contextMock.Object);
        
        // When
        var response = await controller.Index();

        // Then
        Assert.Equal(1, response.Value[0].Id);
        Assert.Equal("Tenant 1", response.Value[0].Name);
    }

    [Fact]
    public async Task Create_AddsNewTenantRecord()
    {
        // Arrange
        var mockContext = new Mock<RemanContext>();
        mockContext.Setup(m => m.Tenants.Add(It.IsAny<Tenant>()));
        mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()))
            .ReturnsAsync(1);

        var controller = new TenantController(mockContext.Object);

        var tenant = new Tenant { Name = "Test Tenant" };

        // Act
        var result = await controller.Create(tenant);

        // Assert
        Assert.Equal("Test Tenant", result.Value.Name);
    }
}