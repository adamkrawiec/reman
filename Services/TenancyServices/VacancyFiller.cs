using reman.Models;

namespace reman.Services.TenancyServices;

public class VacancyFiller
{
    private List<Tenancy> _tenancies;

    public VacancyFiller(List<Tenancy> tenancies)
    {
      _tenancies = tenancies;
    }

    public async Task<List<Tenancy>> AddVacancies()
    {
        if (_tenancies.Count == 0)
            return _tenancies;

        var tenanciesWithVanccies = new List<Tenancy>();

        var latestTenancy = _tenancies.First();
        if(latestTenancy.EndDate is not null)
            tenanciesWithVanccies.Add(addOngoingVacancy(latestTenancy));

        for(int i = 0; i < (_tenancies.Count - 1); i++)
        {
            Tenancy currentTenant = _tenancies[i];
            Tenancy prevTenant = _tenancies[i + 1];
            
            tenanciesWithVanccies.Add(currentTenant);
            Tenancy? vacancy = addVacancy(currentTenant, prevTenant);
            if(vacancy is not null)
                tenanciesWithVanccies.Add(vacancy);

        }
        tenanciesWithVanccies.Add(_tenancies[_tenancies.Count - 1]);
        return tenanciesWithVanccies;
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
            endDate.AddDays(1)
        );
    }
}