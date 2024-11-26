namespace MedicalSystem.API.Contracts.cs.Prescription
{
	public record CreatePrescriptionRequest
	(
		string ItemPrice,
		string Dosage,
		string Instraction,
		int Quantity,
		int Amout
	);
}
