namespace reman.Models;

public class Tenancy
{
    public int Id { get; set; }
    public EstateUnit EstateUnit { get; set; }
    public int EstateUnitId { get; set; }
    public Tenant Tenant { get; set; }
    public int TenantId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public Tenancy() {}

    public Tenancy(EstateUnit estateUnit, Tenant tenant, DateOnly startDate, DateOnly? endDate = null)
    {
        EstateUnit = estateUnit;
        Tenant = tenant;
        StartDate = startDate;
        EndDate = endDate;
    }
}