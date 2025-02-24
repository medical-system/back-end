using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Contracts.cs.MedicalRecord;
using MedicalSystem.API.Contracts.cs.Patients;
using MedicalSystem.API.Entities;

namespace MedicalSystem.API.Services
{
	public interface IPatientService
	{
		public Task<IEnumerable<PatientsResponse>> GetAllAsync(CancellationToken cancellationToken = default);
		public Task<Result<PatientsResponse>> GetAsync(string id);
		public Task<Result<PatientsResponse>> AddAsync(string doctorId, CreatePatientRequest request, CancellationToken cancellationToken = default);
		public Task<int> GetTotalPatientsAsync(CancellationToken cancellationToken = default);
		public Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordAsync(string userId, CancellationToken cancellationToken = default);
		public Task<Result<MedicalRecordResponse>> GetPatientMedicalRecord(string id, CreateMedicalRecordRequest request, CancellationToken cancellationToken = default);
	}
}
