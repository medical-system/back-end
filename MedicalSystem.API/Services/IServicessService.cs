using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Contracts.cs.Servicess;

namespace MedicalSystem.API.Services
{
    public interface IServicessService
    {
        Task<IEnumerable<ServicessResponse>> GetServicesAsync(CancellationToken cancellationToken = default);
        Task<Result<ServicessResponse>> GetServicessByIdAsync(int id);
        Task<Result<ServicessResponse>> AddServicessAsync(ServicessRequest request);
    }
}
