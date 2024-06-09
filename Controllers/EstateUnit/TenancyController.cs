using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reman.Data;
using reman.DTO;
using reman.Services;

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
        var tenancies = await tenancyLoader.GetTenantsWithVacancies();

        return tenancies.Select(t => new TenancyDTO(t)).ToList();
    }
}