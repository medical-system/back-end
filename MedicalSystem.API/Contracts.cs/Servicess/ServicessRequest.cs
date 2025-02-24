using MedicalSystem.API.Entities;

namespace MedicalSystem.API.Contracts.cs.Servicess
{
	public record ServicessRequest
	(
		string Name,
		decimal Price,
		string Descriptions,
		MedicineStatus Status
	);
}
