using System.Runtime.Serialization;

namespace MedicalSystem.API.Entities
{
	public enum MedicineStatus
	{
		[EnumMember(Value = "Available")]
		Available,
		[EnumMember(Value = "Out of stock")]
		OutOfStock,
	}
}
