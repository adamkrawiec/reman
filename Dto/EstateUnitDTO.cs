using reman.Models;
using System.Text.Json.Serialization;


namespace reman.Dto
{
    public class EstateUnitDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EstateUnitType Type { get; set; }
        public RealEstateDTO? RealEstate { get; set; }

        public EstateUnitDTO(EstateUnit estateUnit)
        {
            Id = estateUnit.Id;
            Name = estateUnit.Name;
            Type = estateUnit.Type;
            if(estateUnit.RealEstate is not null)
            {
                RealEstate = new RealEstateDTO(estateUnit.RealEstate);
            }
        }
    }

}