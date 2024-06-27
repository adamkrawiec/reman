
using Microsoft.AspNetCore.Mvc.ModelBinding;
using reman.Data;
using reman.DTO;
using reman.Models;

namespace reman.Services.TenancyServices;

public class TenancyCreator
{
    public ModelStateDictionary ModelState;

    private RemanContext _context;
    private readonly TenancyDTO _tenancyDto;
    private EstateUnit? _estateUnit;
    private Tenant? _tenant;

    public TenancyCreator(
        RemanContext context,
        ModelStateDictionary modelState,
        TenancyDTO tenancyDto
    )
    {
        _context = context;
        ModelState = modelState;
        _tenancyDto = tenancyDto;
    }

    public async Task<TenancyDTO?> SaveTenancy()
    {
        await findEstateUnit();
        await findTenant();
        Tenancy tenancy = createTenancy();

        if(validate().IsValid)
        {
            _context.Tenancies.Add(tenancy);
            await _context.SaveChangesAsync();
            return new TenancyDTO(tenancy);
        }

        return null;
    }

    private ModelStateDictionary validate()
    {
        ModelState.Clear();

        if(_estateUnit is null || _tenant is null)
        { 
            if (_tenant is null) { ModelState.AddModelError("Tenant", "Tenant not found"); };
            if (_estateUnit is null) { ModelState.AddModelError("EstateUnit", "EstateUnit not found"); };
        }

        return ModelState;
    }

    private async Task findEstateUnit()
    {
        _estateUnit = await _context.EstateUnits.FindAsync(_tenancyDto.EstateUnitId);
    }

    private async Task findTenant()
    {
        _tenant = await _context.Tenants.FindAsync(_tenancyDto.TenantId);
    }

    private Tenancy createTenancy()
    {
        Tenancy tenancy = new Tenancy {
            Tenant = _tenant,
            EstateUnit = _estateUnit,
            StartDate = _tenancyDto.StartDate,
            EndDate = _tenancyDto.EndDate
        };
        return tenancy;
    }
}