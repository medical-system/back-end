using Mapster;
using MedicalSystem.API.Contracts.cs.MedicalRecord;
using MedicalSystem.API.Contracts.cs.Prescription;
using MedicalSystem.API.Entities;

namespace MedicalSystem.API.Mapping
{
	public class MappingConfiguration : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<MedicalRecord, MedicalRecordResponse>()
				.Map(dest => dest.Prescriptions, src => src.Prescriptions.Adapt<List<PresciptionResponse>>());

			config.NewConfig<Prescription, PresciptionResponse>();

		}
	}
}
