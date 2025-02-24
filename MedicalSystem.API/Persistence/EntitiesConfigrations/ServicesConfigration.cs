using MedicalSystem.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalSystem.API.Persistence.EntitiesConfigrations
{
	public class ServicesConfigration : IEntityTypeConfiguration<Service>
	{
		public void Configure(EntityTypeBuilder<Service> builder)
		{
			builder.Property(m => m.Id)
				.ValueGeneratedOnAdd()
				.UseIdentityColumn(1, 1);
			builder.Property(m => m.Price)
				.HasColumnType("decimal(18,2)");

			builder.Property(service => service.Status)
				.HasConversion
				(
					(OStatus) => OStatus.ToString(),
					(OStatus) => (ServiceStatus)Enum.Parse(typeof(ServiceStatus), OStatus)
				);
		}
	}
}
