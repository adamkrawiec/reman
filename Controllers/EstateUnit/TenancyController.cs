using Microsoft.AspNetCore.Mvc;
using reman.Data;
using reman.DTO;
using reman.Models;
using reman.Services.TenancyServices;

namespace reman.Controllers;

[ApiController]
[Route("estate-units/{id}/tenants")]
public class EstateUnitTenancyController : ControllerBase
{
    private readonly RemanContext _context;

    public EstateUnitTenancyController(RemanContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<TenancyDTO>>> GetTenancies(int id)
    {
        TenancyLoader tenancyLoader = new TenancyLoader(_context, id);
        List<Tenancy> tenancies = await tenancyLoader.LoadTenancies();

        VacancyFiller vacancyFiller = new VacancyFiller(tenancies);
        List<Tenancy> tenanciesWithVanccies = await vacancyFiller.AddVacancies();

        return tenanciesWithVanccies.Select(t => new TenancyDTO(t)).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<TenancyDTO>> CreateTenancy(int id, TenancyDTO tenancyDto)
    {
        tenancyDto.EstateUnitId = id;
        TenancyCreator tc = new TenancyCreator(_context, ModelState, tenancyDto);
        TenancyDTO? newTenancyDto = await tc.SaveTenancy();

        if(newTenancyDto is not null) { return newTenancyDto; }
        return BadRequest(tc.ModelState);
    }
}