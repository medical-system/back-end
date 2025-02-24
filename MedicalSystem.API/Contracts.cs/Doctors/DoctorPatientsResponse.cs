namespace MedicalSystem.API.Contracts.cs.Doctors
{
	public record DoctorPatientsResponse
	(
		string Id,
		string FullName,
		DateTime CreatedAt,
		string Gender
	);
}
