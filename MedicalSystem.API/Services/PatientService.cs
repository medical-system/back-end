using Mapster;
using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Abstractions.Consts;
using MedicalSystem.API.Contracts.cs.MedicalRecord;
using MedicalSystem.API.Contracts.cs.Patients;
using MedicalSystem.API.Entities;
using MedicalSystem.API.Errors;
using MedicalSystem.API.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.API.Services
{
	public class PatientService : IPatientService
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<PatientService> _logger;
		public PatientService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<PatientService> logger)
		{
			_context = context;
			_userManager = userManager;
			_logger = logger;
		}
		public async Task<IEnumerable<PatientsResponse>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var patients = await (from u in _context.Users
								  join ur in _context.UserRoles
								  on u.Id equals ur.UserId
								  join r in _context.Roles
								  on ur.RoleId equals r.Id
								  where r.Name == DefaultRoles.Patient && u.IsDisabled == false
								  select new PatientsResponse
								  (
									 u.Id,
									 u.FullName,
									 u.CreatedAt,
									 u.BloodyGroup,
									 u.Age,
									 u.IsDisabled
								  )).ToListAsync(cancellationToken);
			return patients;
		}

		public async Task<Result<PatientsResponse>> GetAsync(string id)
		{
			if (await _userManager.FindByIdAsync(id) is not { } user)
				return Result.Failure<PatientsResponse>(UserErrors.UserNotFound);

			var userRole = await _userManager.GetRolesAsync(user);
			if (!userRole.Contains(DefaultRoles.Patient))
				return Result.Failure<PatientsResponse>(UserErrors.UserNotFound);

			var response = user.Adapt<PatientsResponse>();
			return Result.Success(response);
		}

		public async Task<Result<PatientsResponse>> AddAsync(CreatePatientRequest request, CancellationToken cancellationToken = default)
		{
			var emailIsExists = await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
			if (emailIsExists)
				return Result.Failure<PatientsResponse>(UserErrors.DuplicatedEmail);

			var user = request.Adapt<ApplicationUser>();
			string fullName = request.FullName;
			string[] nameParts = fullName.Split(' ');
			user.FirstName = nameParts[0];
			user.LastName = nameParts.Length > 1 ? nameParts[^1] : string.Empty;
			user.Email = request.Email;
			user.UserName = request.Email;
			user.Password = request.Password;
			user.EmailConfirmed = true;
			user.FullName = request.FullName;
			user.BloodyGroup = request.BloodyGroup;
			user.Age = request.Age;

			var result = await _userManager.CreateAsync(user, request.Password);

			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, DefaultRoles.Patient);

				var response = (user).Adapt<PatientsResponse>();

				return Result.Success(response);
			}

			var error = result.Errors.First();

			return Result.Failure<PatientsResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

		}

		public async Task<Result> ToggleStatus(string id)
		{
			if (await _userManager.FindByIdAsync(id) is not { } user)
				return Result.Failure<PatientsResponse>(UserErrors.UserNotFound);

			user.IsDisabled = !user.IsDisabled;
			var result = await _userManager.UpdateAsync(user);
			if (result.Succeeded)
				return Result.Success();

			var error = result.Errors.First();
			return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
		}
		public async Task<int> GetTotalPatientsAsync(CancellationToken cancellationToken = default)
		{
			var totalPatients = await (from u in _context.Users
									   join ur in _context.UserRoles
									   on u.Id equals ur.UserId
									   join r in _context.Roles
									   on ur.RoleId equals r.Id
									   where r.Name == DefaultRoles.Patient && u.IsDisabled == false
									   select u.Id).CountAsync(cancellationToken);
			return totalPatients;
		}
		public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordAsync(string userId, CancellationToken cancellationToken = default)
		{
			var medicalRecords = await _context.MedicalRecords
										.Where(mr => mr.PatientId == userId)
										.Include(mr => mr.Prescriptions)
										.ToListAsync(cancellationToken);

			return medicalRecords;
		}

		public async Task<Result<MedicalRecordResponse>> GetPatientMedicalRecord(string id, CreateMedicalRecordRequest request, CancellationToken cancellationToken = default)
		{
			if (await _userManager.FindByIdAsync(id) is not { } user)
				return Result.Failure<MedicalRecordResponse>(UserErrors.UserNotFound);

			var userRole = await _userManager.GetRolesAsync(user);
			if (!userRole.Contains(DefaultRoles.Patient))
				return Result.Failure<MedicalRecordResponse>(UserErrors.UserNotFound);

			var medicalRecord = new MedicalRecord
			{
				PatientId = id,
				Complaint = request.Complaint,
				Date = request.Date,
				Diagnosis = request.Diagnosis,
				Treatment = request.Treatment,
				VitalSigns = request.VitalSigns,
				Prescriptions = request.Prescriptions.Select(p => new Prescription
				{
					ItemPrice = p.ItemPrice,
					Dosage = p.Dosage,
					Instraction = p.Instraction,
					Quantity = p.Quantity,
					Amout = p.Amout
				}).ToList()
			};

			_context.MedicalRecords.Add(medicalRecord);
			await _context.SaveChangesAsync(cancellationToken);

			var response = medicalRecord.Adapt<MedicalRecordResponse>();

			return Result.Success(response);
		}
	}
}
