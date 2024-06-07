using reman.Models;

namespace reman.Data.Seeds;

class Tenants
{
    private readonly RemanContext _context;
    public Tenants(RemanContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        if (_context.Tenants.Any())
        {
            return;
        }

        var tenants = new Tenant[]
        {
            new Tenant { Name = "John Doe" },
            new Tenant { Name = "Jane Doe" },
            new Tenant { Name = "Alex Smith" },
            new Tenant { Name = "Abby Smith" },
            new Tenant { Name = "Mark Johnson" },
            new Tenant { Name = "Tanya Johnson" },
            new Tenant { Name = "John Brown" },
            new Tenant { Name = "Jane Brown" },

        };
        _context.AddRange(tenants);

        _context.SaveChanges();
    }
}