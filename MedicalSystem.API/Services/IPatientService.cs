using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Contracts.cs.Patients;

namespace MedicalSystem.API.Services
{
	public interface IPatientService
	{
		public Task<IEnumerable<PatientsResponse>> GetAllAsync(CancellationToken cancellationToken = default);
		public Task<Result<PatientsResponse>> GetAsync(string id);
		public Task<Result<PatientsResponse>> AddAsync(CreatePatientRequest request, CancellationToken cancellationToken = default);
		public Task<Result> ToggleStatus(string id);
	}
}
