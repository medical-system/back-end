using Mapster;
using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Contracts.cs.Servicess;
using MedicalSystem.API.Entities;
using MedicalSystem.API.Errors;
using MedicalSystem.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.API.Services
{
	public class ServicessService : IServicessService
	{
		private readonly ApplicationDbContext _context;
		public ServicessService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<ServicessResponse>> GetServicesAsync(CancellationToken cancellationToken = default)
		{
			var servicess = await _context.Servicess
				.Select(s => new ServicessResponse(
					s.Id,
					s.Name,
					s.CreatedOn,
					s.Price,
					s.Status)).ToListAsync(cancellationToken);
			return servicess!;
		}
		public async Task<Result<ServicessResponse>> GetServicessByIdAsync(int id)
		{
			var IsExist = await _context.Servicess.AnyAsync(x => x.Id == id && x.Status != Status.Disabled);
			if (!IsExist)
				return Result.Failure<ServicessResponse>(ServicessErrors.ServiceNotFound);
			var servicess = await _context.Servicess.FindAsync(id);
			var response = new ServicessResponse(
				servicess.Id,
				servicess.Name,
				servicess.CreatedOn,
				servicess.Price,
				servicess.Status);
			return Result.Success(response);
		}

		public async Task<Result<ServicessResponse>> AddServicessAsync(ServicessRequest request)
		{
			var isExist = await _context.Servicess.AnyAsync(x => x.Name == request.Name);
			if (isExist)
				return Result.Failure<ServicessResponse>(ServicessErrors.DuplicatedName);

			var response = request.Adapt<Service>();
			_context.Servicess.Add(response);
			await _context.SaveChangesAsync();

			return Result.Success(request.Adapt<ServicessResponse>());
		}
	}

}
