using MedicalSystem.API.Contracts.cs.Receptions;

namespace MedicalSystem.API.Services
{
	public interface IReceptionService
	{
		public Task<IEnumerable<ReceptionResponse>> GetAllAsync(CancellationToken cancellationToken = default);
	}
}
