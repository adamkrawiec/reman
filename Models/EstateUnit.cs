
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace reman.Models;

public enum EstateUnitType {
	COMMERCIAL,
	RESIDENTIAL
}

public class EstateUnit {
	public int Id { get; set; }

	[Required]
	public string Name { get; set; }
	
	public int RealEstateId { get; set; }
	public RealEstate? RealEstate { get; set; }

	[JsonConverter(typeof(JsonStringEnumConverter))]
	public EstateUnitType Type { get; set; }

	public int FlatNumber { get; set; }

	public float Area { get; set; }

	public EstateUnit()
	{
	}

	public EstateUnit(
		int id,
		string name,
		int realEstateId,
		int flatNumber,
		float area,
		EstateUnitType type = EstateUnitType.RESIDENTIAL
	)
	{
		Id = id;
		Name = name;
		RealEstateId = realEstateId;
		Type = type;
		FlatNumber = flatNumber;
		Area = area;
	}
}