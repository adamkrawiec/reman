using reman.Data.Seeds;

namespace reman.Data;

class Seeder
{
    private readonly RemanContext _context;

    public Seeder(RemanContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        _context.Database.EnsureCreated();

        var realEstates = new RealEstates(_context);
        var estateUnits = new EstateUnits(_context);
        realEstates.Initialize();
        estateUnits.Initialize();
    }
}