using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reman.Data;
using reman.DTO;
using reman.Models;

namespace reman.Controllers;

[ApiController]
[Route("estate-units")]
public class EstateUnitTenancyController : ControllerBase
{
    private readonly RemanContext _context;

    public EstateUnitTenancyController(RemanContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/tenants")]
    public async Task<ActionResult<List<TenancyDTO>>> GetTenants(int id)
    {
        var tenancies = await _context.Tenancies
        .Where(t => t.EstateUnit.Id == id)
        .Include(t => t.Tenant)
        .OrderByDescending(t => t.EndDate)
        .ToListAsync();

        return tenancies.Select(t => new TenancyDTO(t)).ToList();
    }
}