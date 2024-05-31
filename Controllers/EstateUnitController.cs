using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reman.Data;
using reman.Models;

namespace reman.Controllers;

[ApiController]
[Route("estate-units")]
public class EstateUnitController : ControllerBase
{
  private readonly RemanContext _context;

  public EstateUnitController(RemanContext context)
  {
    _context = context;
  }
  
  [HttpGet(Name = "EstateUnit")]
  public async Task<ActionResult<List<EstateUnit>>> Index()
  {
    var estateUnits = await _context.EstateUnits.ToListAsync();
    return estateUnits;
  }
}