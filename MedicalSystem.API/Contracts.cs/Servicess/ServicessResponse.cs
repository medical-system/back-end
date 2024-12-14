using MedicalSystem.API.Entities;

namespace MedicalSystem.API.Contracts.cs.Servicess
{
	public record ServicessResponse
	(
		int Id,
		string Name,
		DateTime CreatedAt,
		decimal Price,
		Status Status
	);
}
