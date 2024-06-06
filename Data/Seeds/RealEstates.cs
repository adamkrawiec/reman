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
            new RealEstate { Name = "RE00001", City = "New York" },
            new RealEstate { Name = "RE00002", City = "London" },
            new RealEstate { Name = "RE00003", City = "Paris" }
        };
        _context.AddRange(realEstates);

        _context.SaveChanges();
    }
}