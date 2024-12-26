namespace MedicalSystem.API.Contracts.cs.Receptions
{
	public record ReceptionResponse
	(
		string Id,
		string Name,
		DateTime CreatedAt,
		string Phone,
		string Title,
		string Email
	);
}
