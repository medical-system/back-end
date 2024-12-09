using MedicalSystem.API.Entities;

namespace MedicalSystem.API.Contracts.cs.Medicines
{
	public record MedicineResponse
	(
		string Name,
		decimal Price,
		Status Status,
		int InStock,
		int Measure
	);
}
