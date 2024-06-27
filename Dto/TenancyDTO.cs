using reman.Dto;
using reman.Models;

namespace reman.DTO;

public class TenancyDTO
{
    public int Id { get; set; }
    public EstateUnitDTO? EstateUnit { get; set; }
    public TenantDTO? Tenant { get; set; }
    public int EstateUnitId { get; set; }
    public int TenantId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public TenancyDTO() {}

    public TenancyDTO(Tenancy tenancy)
    {
        Id = tenancy.Id;
        TenantId = tenancy.TenantId;
        Tenant = new TenantDTO(tenancy.Tenant);
        StartDate = tenancy.StartDate;
        EndDate = tenancy.EndDate;
        if(tenancy.EstateUnit is not null)
        {
            EstateUnit = new EstateUnitDTO(tenancy.EstateUnit);
        }
    }

    public TenancyDTO(EstateUnitDTO? estateUnit, TenantDTO tenant, DateOnly startDate, DateOnly? endDate)
    {
        Tenant = tenant;
        StartDate = startDate;
        EndDate = endDate;
        if(estateUnit is not null)
        {
            EstateUnit = estateUnit;
        }
    }
}