using reman.Models;

namespace reman.Dto;

public class RealEstateDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Street { get; set; }

    public int HouseNumber { get; set; }

    public RealEstateDTO(RealEstate realEstate)
    {
        Id = realEstate.Id;
        Name = realEstate.Name;
        City = realEstate.City;
        Street = realEstate.Street;
        HouseNumber = realEstate.HouseNumber;
    }
}