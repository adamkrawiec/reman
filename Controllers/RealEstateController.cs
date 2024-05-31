using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reman.Data;
using reman.Models;

namespace reman.Controllers;

[ApiController]
[Route("real-estates")]
public class RealEstateController : ControllerBase
{
  private readonly RemanContext _context;
  public RealEstateController(RemanContext context)
  {
    _context = context;
  }

  [HttpGet(Name = "RealEstate")]
  public async Task<ActionResult<List<RealEstate>>> Index()
  {
    var realEstates = await _context.RealEstates.ToListAsync();
    return realEstates;
  }

  [HttpPost]
  public async Task<ActionResult<RealEstate>> Create(RealEstate realEstate)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }
    _context.RealEstates.Add(realEstate);
    await _context.SaveChangesAsync();
    return realEstate;
  }
}