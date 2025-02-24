using MedicalSystem.API.Entities;

namespace MedicalSystem.API.Contracts.cs.Medicines
{
	public record CreateMedicineRequest
	(
		string Name,
		decimal Price,
		MedicineStatus Status,
		int InStock,
		int Measure
	);
}
