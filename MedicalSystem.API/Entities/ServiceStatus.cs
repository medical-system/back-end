using System.Runtime.Serialization;

namespace MedicalSystem.API.Entities
{
	public enum ServiceStatus
	{
		[EnumMember(Value = "Enabled")]
		Enabled,
		[EnumMember(Value = "Disabled")]
		Disabled
	}
}
