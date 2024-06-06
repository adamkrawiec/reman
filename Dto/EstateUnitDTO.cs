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

        public int FlatNumber { get; set; }

        public float Area { get; set; }

        public string Address {
            get {
                if(RealEstate is null)
                {
                    return $"{FlatNumber}";
                }

                return $"{RealEstate?.City}, {RealEstate?.Street} {RealEstate?.HouseNumber}/{FlatNumber}";
            }
        }

        public EstateUnitDTO()
        {
        }

        public EstateUnitDTO(EstateUnit estateUnit)
        {
            Id = estateUnit.Id;
            Name = estateUnit.Name;
            Type = estateUnit.Type;
            FlatNumber = estateUnit.FlatNumber;
            Area = estateUnit.Area;
            if(estateUnit.RealEstate is not null)
            {
                RealEstate = new RealEstateDTO(estateUnit.RealEstate);
            }
        }
    }

}