using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reman.Data;
using reman.Models;

namespace reman.Controllers;

[ApiController]
[Route("tenants")]
public class TenantController : ControllerBase
{
    private readonly RemanContext _context;

    public TenantController(RemanContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "Tenants")]
    public async Task<ActionResult<List<Tenant>>> Index()
    {
        var tenants = await _context.Tenants.ToListAsync();
        return tenants;
    }

    [HttpPost]
    public async Task<ActionResult<Tenant>> Create(Tenant tenant)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _context.Tenants.Add(tenant);
        await _context.SaveChangesAsync();
        return tenant;
    }
}