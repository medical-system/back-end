namespace MedicalSystem.API.Contracts.cs.Patients
{
	public record PatientsResponse
	(
		string Id,
		string FullName,
		DateTime CreatedAt,
		string BloodyGroup,
		int Age,
		bool IsDisabled
	);
}
