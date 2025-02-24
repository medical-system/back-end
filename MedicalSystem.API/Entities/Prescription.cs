using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.API.Entities
{
    [Owned]
	public class Prescription
	{
        public int Item { get; set; }
        public string ItemPrice { get; set; } = string.Empty;
		public string Dosage { get; set; } = string.Empty;
		public string Instraction { get; set; } = string.Empty;
		public int Quantity { get; set; }
        public int Amout { get; set; }
    }
}
