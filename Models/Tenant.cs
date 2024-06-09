namespace reman.Models;

public class Tenant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Commercial { get; set; }

    public Tenant()
    {
    }

    public static Tenant Vacancy()
    {
        return new Tenant { Name = "Vacancy" };
    }

    public Tenant(int id, string name, bool commercial = false)
    {
        Id = id;
        Name = name;
        Commercial = commercial;
    }
}