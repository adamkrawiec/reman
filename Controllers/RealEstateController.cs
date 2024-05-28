using Microsoft.AspNetCore.Http.HttpResults;
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
}