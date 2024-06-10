using Microsoft.AspNetCore.Mvc;
using reman.Data;
using reman.DTO;
using reman.Services.TenancyServices;

namespace reman.Controllers;

[ApiController]
[Route("estate-units")]
public class EstateUnitTenancyController : ControllerBase
{
    private readonly RemanContext _context;
    private readonly TenancyLoader _tenancyLoader;

    public EstateUnitTenancyController(RemanContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/tenants")]
    public async Task<ActionResult<List<TenancyDTO>>> GetTenancies(int id)
    {
        TenancyLoader tenancyLoader = new TenancyLoader(_context, id);
        var tenancies = await tenancyLoader.LoadTenancies();

        VacancyFiller vacancyFiller = new VacancyFiller(tenancies);
        var tenanciesWithVanccies = await vacancyFiller.AddVacancies();

        return tenancies.Select(t => new TenancyDTO(t)).ToList();
    }
}