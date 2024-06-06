    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using reman.Data;
    using reman.Dto;
    using reman.Models;

    namespace reman.Controllers;

    [ApiController]
    [Route("estate-units")]
    public class EstateUnitController : ControllerBase
    {
    private readonly RemanContext _context;

    public EstateUnitController(RemanContext context)
    {
        _context = context;
    }
    
    [HttpGet(Name = "EstateUnit")]
    public async Task<ActionResult<List<EstateUnitDTO>>> Index()
    {
        var estateUnits = await _context.EstateUnits.Include(eu => eu.RealEstate).ToListAsync();
        return estateUnits.Select(eu => new EstateUnitDTO(eu)).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<EstateUnitDTO>> Create(EstateUnit estateUnit)
    {
        RealEstate? realEstate = await _context.RealEstates.FindAsync(estateUnit.RealEstateId);
        if(realEstate == null)
        {
            return BadRequest("Real Estate not found");
        }

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        estateUnit.RealEstate = realEstate;
        _context.EstateUnits.Add(estateUnit);
        await _context.SaveChangesAsync();
        return new EstateUnitDTO(estateUnit);
    }

    [HttpGet("{id}", Name = "EstateUnitById")]
    public async Task<ActionResult<EstateUnitDTO>> GetById(int id)
    {
        var estateUnit = await _context.EstateUnits.Include(eu => eu.RealEstate).FirstOrDefaultAsync(eu => eu.Id == id);
        if (estateUnit == null)
        {
        return NotFound();
        }
        return new EstateUnitDTO(estateUnit);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EstateUnitDTO>> UpdateById(int id, EstateUnit estateUnitParams)
    {
        var estateUnit = await _context.EstateUnits.FindAsync(id);
        if (estateUnit == null)
        {
            return NotFound();
        }

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Entry(estateUnit).CurrentValues.SetValues(estateUnitParams);
        await _context.SaveChangesAsync();
        return new EstateUnitDTO(estateUnitParams);
    }
}