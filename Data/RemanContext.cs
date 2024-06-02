using reman.Models;
using Microsoft.EntityFrameworkCore;

namespace reman.Data
{
    public class RemanContext : DbContext
    {
        public RemanContext() { }

        public RemanContext(DbContextOptions<RemanContext> options) : base(options)
        {
        }

        public virtual DbSet<RealEstate> RealEstates { get; set; } = null;
        public virtual DbSet<EstateUnit> EstateUnits { get; set; } = null;
    }
}