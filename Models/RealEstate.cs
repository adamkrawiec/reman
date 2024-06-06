using System.ComponentModel.DataAnnotations;

namespace reman.Models;

public class RealEstate
{
  public int Id { get; set; }
  
  [Required]
  public string Name { get; set; }

  [Required]
  public string City { get; set; }

  public string Street { get; set; }

  public int HouseNumber { get; set; }

  public RealEstate()
  {
  }

  public RealEstate(int id, string name, string city, string street, int houseNumber)
  {
    Id = id;
    Name = name;
    City = city;
    Street = street;
    HouseNumber = houseNumber;
  }
}