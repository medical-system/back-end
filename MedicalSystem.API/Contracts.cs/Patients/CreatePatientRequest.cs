namespace MedicalSystem.API.Contracts.cs.Patients
{
	public record CreatePatientRequest
	(
		string Email,
		string FullName,
		string Password,
		IFormFile? ImageUrl,
		int Age,
		string BloodyGroup
	);
}
