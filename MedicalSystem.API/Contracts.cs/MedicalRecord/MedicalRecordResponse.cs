using MedicalSystem.API.Contracts.cs.Prescription;

namespace MedicalSystem.API.Contracts.cs.MedicalRecord
{
	public record MedicalRecordResponse
	(
		int id,
		string PatientId,
		string Complaint,
		string Diagnosis,
		string Treatment,
		string VitalSigns,
		List<PresciptionResponse> Prescriptions
	);
}
