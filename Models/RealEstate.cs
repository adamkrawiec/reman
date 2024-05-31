using System.ComponentModel.DataAnnotations;

namespace reman.Models;

public class RealEstate
{
  public int Id { get; set; }
  
  [Required]
  public string Name { get; set; }

  public RealEstate()
  {
  }

  public RealEstate(int id, string name)
  {
    Id = id;
    Name = name;
  }
}