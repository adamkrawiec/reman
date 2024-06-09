using reman.Models;

namespace reman.DTO;

public class TenantDTO {
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Commercial { get; set; }

    public TenantDTO() {}

    public TenantDTO(int id, string name, bool commercial = false) {
        Id = id;
        Name = name;
        Commercial = commercial;
    }

    public TenantDTO(Tenant tenant){
        Id = tenant.Id;
        Name = tenant.Name;
        Commercial = tenant.Commercial;
    }
}