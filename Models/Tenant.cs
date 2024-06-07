namespace reman.Models;

public class Tenant
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Tenant()
    {
    }

    public Tenant(int id, string name)
    {
        Id = id;
        Name = name;
    }
}