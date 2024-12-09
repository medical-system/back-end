using MedicalSystem.API.Entities;

namespace MedicalSystem.API.Contracts.cs.Medicines
{
	public record CreateMedicineRequest
	(
		string Name,
		decimal Price,
		Status Status,
		int InStock,
		int Measure
	);
}
