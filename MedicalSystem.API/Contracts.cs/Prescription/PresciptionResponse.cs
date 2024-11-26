namespace MedicalSystem.API.Contracts.cs.Prescription
{
	public record PresciptionResponse
	(
		string ItemPrice,
		string Dosage,
		string Instraction,
		int Quantity,
		int Amout
	);
}
