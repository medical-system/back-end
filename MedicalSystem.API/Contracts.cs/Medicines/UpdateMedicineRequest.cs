using MedicalSystem.API.Entities;

namespace MedicalSystem.API.Contracts.cs.Medicines
{
	public record UpdateMedicineRequest
	(
		string Name,
		decimal Price,
		MedicineStatus Status,
		int InStock,
		int Measure
	);
}
