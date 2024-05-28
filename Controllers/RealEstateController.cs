using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using reman.Models;

namespace reman.Controllers;

[ApiController]
[Route("real-estates")]
public class RealEstateController : ControllerBase
{
  public RealEstateController()
  {

  }

  [HttpGet(Name = "RealEstate")]
  public ActionResult<List<RealEstate>> Index()
  {
    RealEstate re1 = new RealEstate { Id = 1, Name = "Real Estate 1" };
    List<RealEstate> realEstates = new List<RealEstate>() { re1 };

    return realEstates;
  }
}