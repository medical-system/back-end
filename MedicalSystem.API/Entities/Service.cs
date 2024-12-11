namespace MedicalSystem.API.Entities
{
	public class Service : AuditableEntity
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
    }
}
