using Microsoft.EntityFrameworkCore;
using reman.Data;
using reman.DTO;
using reman.Models;

namespace reman.Services.TenancyServices;

public class TenancyLoader
{
    private readonly int _estateUnitId;
    private readonly RemanContext _context;

    public TenancyLoader(RemanContext remanContext, int estateUnitId)
    {
        _context = remanContext;
        _estateUnitId = estateUnitId;
    }

    public async Task<List<Tenancy>> LoadTenancies()
    {
        List<Tenancy> tenancies = await _context
            .Tenancies
            .Where(t => t.EstateUnitId == _estateUnitId)
            .Include(t => t.Tenant)
            .OrderByDescending(t => t.EndDate)
            .ToListAsync();
        
        return tenancies;
    }
}