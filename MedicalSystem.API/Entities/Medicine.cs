namespace MedicalSystem.API.Entities
{
	public class Medicine 
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public MedicineStatus Status { get; set; }
		public int InStock { get; set; }
		public int Measure { get; set; }
	}
}
