using reman.Models;

namespace reman.Dto;

public class RealEstateDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public RealEstateDTO(RealEstate realEstate)
    {
        Id = realEstate.Id;
        Name = realEstate.Name;
    }
}