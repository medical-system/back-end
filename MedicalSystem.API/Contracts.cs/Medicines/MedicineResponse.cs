using MedicalSystem.API.Entities;

namespace MedicalSystem.API.Contracts.cs.Medicines
{
	public record MedicineResponse
	(
		string Name,
		decimal Price,
		MedicineStatus Status,
		int InStock,
		int Measure
	);
}
