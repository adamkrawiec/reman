using reman.Models;

namespace reman.Data.Seeds;

class EstateUnits
{
  private readonly RemanContext _context;

  public EstateUnits(RemanContext context)
  {
    _context = context;
  }

  public void Initialize()
  {
    if (_context.EstateUnits.Any())
    {
      return;
    }

    var estateUnits = new EstateUnit[]
    {
      new EstateUnit { Name = "EU00011", RealEstateId = 1, Type = EstateUnitType.RESIDENTIAL },
      new EstateUnit { Name = "EU00012", RealEstateId = 1, Type = EstateUnitType.RESIDENTIAL },
      new EstateUnit { Name = "EUC0013", RealEstateId = 1, Type = EstateUnitType.COMMERCIAL },
      new EstateUnit { Name = "EU00021", RealEstateId = 2, Type = EstateUnitType.RESIDENTIAL },
      new EstateUnit { Name = "EUC0022", RealEstateId = 2, Type = EstateUnitType.COMMERCIAL },
      new EstateUnit { Name = "EU00023", RealEstateId = 2, Type = EstateUnitType.RESIDENTIAL },
      new EstateUnit { Name = "EU00031", RealEstateId = 3, Type = EstateUnitType.RESIDENTIAL },
    };
    _context.AddRange(estateUnits);

    _context.SaveChanges();
  }
}