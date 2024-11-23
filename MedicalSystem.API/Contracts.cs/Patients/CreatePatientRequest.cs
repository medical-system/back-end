namespace MedicalSystem.API.Contracts.cs.Patients
{
	public record CreatePatientRequest
	(
		string Email,
		string FullName,
		string Password,
		int Age,
		string BloodyGroup
	);
}
