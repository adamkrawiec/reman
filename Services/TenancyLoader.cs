using Microsoft.EntityFrameworkCore;
using reman.Data;
using reman.Models;

namespace reman.Services;

public class TenancyLoader
{
    private readonly int _estateUnitId;
    private readonly RemanContext _context;

    public TenancyLoader(RemanContext remanContext, int estateUnitId)
    {
        _context = remanContext;
        _estateUnitId = estateUnitId;
    }

    public async Task<List<Tenancy>> GetTenancies()
    {
        var tenancies = await loadTenancies();
        return tenancies;
    }

    public async Task<List<Tenancy>> GetTenantsWithVacancies()
    {
        var tenancies = await loadTenancies();
        if (tenancies.Count == 0)
            return new List<Tenancy>();

        var tenanciesWithVanccies = new List<Tenancy>();

        var latestTenancy = tenancies.First();
        if(latestTenancy.EndDate is not null)
            tenanciesWithVanccies.Add(addOngoingVacancy(latestTenancy));

        for(int i = 0; i < (tenancies.Count - 1); i++)
        {
            Tenancy currentTenant = tenancies[i];
            Tenancy prevTenant = tenancies[i + 1];
            
            tenanciesWithVanccies.Add(currentTenant);
            Tenancy? vacancy = addVacancy(currentTenant, prevTenant);
            if(vacancy is not null)
                tenanciesWithVanccies.Add(vacancy);

        }
        tenanciesWithVanccies.Add(tenancies[tenancies.Count - 1]);
        return tenanciesWithVanccies;
    }

    private async Task<List<Tenancy>> loadTenancies()
    {
        // List<Tenancy> tenancies = await _context
        //     .Tenancies
        //     .Where(t => t.EstateUnitId == _estateUnitId)
        //     .Include(t => t.Tenant)
        //     .OrderByDescending(t => t.EndDate)
        //     .ToListAsync();
        
        List<Tenancy> tenancies = await _context
            .Tenancies
            .OrderByDescending(t => t.EndDate)
            .ToListAsync();

        return tenancies;
    }

    private Tenancy? addVacancy(Tenancy currentTenant, Tenancy prevTenant)
    {
        if(prevTenant.EndDate is null) return null;

        int endDate = prevTenant.EndDate.Value.DayNumber;
        int startDate = currentTenant.StartDate.DayNumber;

        if(startDate - endDate == 1) return null;
        
        return new Tenancy(
            currentTenant.EstateUnit,
            Tenant.Vacancy(),
            prevTenant.EndDate.Value.AddDays(1),
            currentTenant.StartDate.AddDays(-1)
        );
    }

    private Tenancy addOngoingVacancy(Tenancy latestTenancy)
    {
        DateOnly endDate = latestTenancy.EndDate.Value;
        return new Tenancy(
            latestTenancy.EstateUnit,
            Tenant.Vacancy(),
            latestTenancy.EndDate.Value.AddDays(1)
        );
    }
}