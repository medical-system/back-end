using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Contracts.cs.Users;

namespace MedicalSystem.API.Services
{
	public interface IUserService
	{
		public Task<Result> ToggleStatus(string id);
		public Task<Result> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default);
	}
}
