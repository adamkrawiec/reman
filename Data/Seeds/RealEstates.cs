using reman.Models;

namespace reman.Data.Seeds;

class RealEstates
{
    private readonly RemanContext _context;

    public RealEstates(RemanContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        if (_context.RealEstates.Any())
        {
            return;
        }

        var realEstates = new RealEstate[]
        {
            new RealEstate { Name = "RE00001" },
            new RealEstate { Name = "RE00002" },
            new RealEstate { Name = "RE00003" }
        };
        _context.AddRange(realEstates);

        _context.SaveChanges();
    }
}