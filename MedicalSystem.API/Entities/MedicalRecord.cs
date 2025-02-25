namespace MedicalSystem.API.Entities
{
	public class MedicalRecord : AuditableEntity
	{
		public int Id { get; set; }
        public string? PatientId { get; set; } 
        public string Complaint { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
		public string Treatment { get; set; } = string.Empty;
		public string VitalSigns { get; set; } = string.Empty;
		public List<Prescription>? Prescriptions { get; set; }
    }
}
