namespace reman.Models;

public class RealEstate
{
  public int Id { get; set; }
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