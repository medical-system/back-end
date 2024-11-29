using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Contracts.cs.Medicines;
using MedicalSystem.API.Entities;
using MedicalSystem.API.Errors;
using MedicalSystem.API.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MedicalSystem.API.Services
{
	public class MedicineService : IMedicineService
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<MedicineService> _logger;
		public MedicineService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<MedicineService> logger)
		{
			_context = context;
			_userManager = userManager;
			_logger = logger;
		}
		public async Task<IEnumerable<MedicineResponse>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var medicines = await _context.Medicines
				.Select(m => new MedicineResponse(
					m.Name,
					m.Price,
					m.Status,
					m.InStock,
					m.Measure))
				.ToListAsync(cancellationToken);

			return medicines;
		}
		public async Task<Result<MedicineResponse>> AddMedicineAsync(CreateMedicineRequest request)
		{
			var isExist = await _context.Medicines.AnyAsync(x => x.Name == request.Name);
			if (isExist)
				return Result.Failure<MedicineResponse>(MedicineErrors.DuplicatedName);

			var medicine = new Medicine
			{
				Name = request.Name,
				Price = request.Price,
				Status = request.Status,
				InStock = request.InStock,
				Measure = request.Measure
			};
			_context.Medicines.Add(medicine);
			await _context.SaveChangesAsync();

			var response = new MedicineResponse(
				medicine.Name,
				medicine.Price,
				medicine.Status,
				medicine.InStock,
				medicine.Measure);

			return Result.Success(response);
		}
		public async Task<Result<MedicineResponse>> UpdateMedicineAsync(int id, UpdateMedicineRequest request, CancellationToken cancellationToken = default)
		{
			var medicine = await _context.Medicines.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
			if (medicine is null)
				return Result.Failure<MedicineResponse>(MedicineErrors.MedicineNotFound);

			var isNameExist = await _context.Medicines.AnyAsync(x => x.Id != id && x.Name == request.Name, cancellationToken);
			if (isNameExist)
				return Result.Failure<MedicineResponse>(MedicineErrors.DuplicatedName);

			medicine.Price = request.Price;
			medicine.Status = request.Status;
			medicine.InStock = request.InStock;
			medicine.Measure = request.Measure;

			await _context.SaveChangesAsync(cancellationToken);

			var response = new MedicineResponse(
				medicine.Name,
				medicine.Price,
				medicine.Status,
				medicine.InStock,
				medicine.Measure);

			return Result.Success(response);
		}
	}
}
