using Moq;
using Moq.EntityFrameworkCore;
using reman.Data;
using reman.Models;
using reman.Services.TenancyServices;

namespace reman.Tests.Services;

public class TenancyLoaderTest
{
    private EstateUnit newEstateUnit()
    {
        return new EstateUnit {
            Id = 1,
            Name = "Estate Unit 1",
            RealEstate = new RealEstate { Id = 1, Name = "Real Estate" },
            Area = 100,
            FlatNumber = 1
        };
    }

    private Tenancy newTenancy(EstateUnit estateUnit, DateOnly startDate, DateOnly? endDate)
    {
        return new Tenancy {
            Id = 1,
            EstateUnit = newEstateUnit(),
            Tenant = newTenant(),
            StartDate = startDate,
            EndDate = endDate
        };
    }

    private Tenant newTenant()
    {
        return new Tenant {
            Id = 1,
            Name = "Tenant 1"
        };
    }

    private Mock<RemanContext> setupDBContext(List<Tenancy> tenancies)
    {
        var contextMock = new Mock<RemanContext>();
        contextMock.Setup(c => c.Tenancies).ReturnsDbSet(tenancies);
        return contextMock;
    }

    [Fact]
    public async Task GetTenancies_ReturnsTenanciesListForEstateUnit()
    {
        EstateUnit estateUnit = newEstateUnit();
        List<Tenancy> tenancies = new List<Tenancy> {
            newTenancy(estateUnit, new DateOnly(2020,1,1), new DateOnly(2020,10,1)),
            newTenancy(estateUnit, new DateOnly(2020,1,1), new DateOnly(2020,10,1))
        };
        Mock<RemanContext> contextMock = setupDBContext(tenancies);

        TenancyLoader tenancyLoader = new TenancyLoader(contextMock.Object, estateUnit.Id);
    }

    // public async Task GetTenantsWithVacancies_ReturnTenanciesListForEstateUnit()
    // {

    // }

    // public async Task GetTenantsWithVacancies_ReturnTenanciesListWithAddedVacanciesForEstateUnit()
    // {

    // }

    // public async Task GetTenantsWithVacancies_ReturnTenanciesListWithAddedOngoingVacancyForEstateUnit()
    // {
        
    // }
}
