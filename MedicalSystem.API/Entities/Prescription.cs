namespace MedicalSystem.API.Entities
{
	public class Prescription
	{
        public int Id { get; set; }
        public int Item { get; set; }
        public int MedicalRecordId { get; set; }
        public string ItemPrice { get; set; }
        public string Dosage { get; set; }
        public string Instraction { get; set; }
        public int Quantity { get; set; }
        public int Amout { get; set; }
    }
}
