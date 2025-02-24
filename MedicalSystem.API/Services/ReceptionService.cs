using MedicalSystem.API.Abstractions.Consts;
using MedicalSystem.API.Contracts.cs.Patients;
using MedicalSystem.API.Contracts.cs.Receptions;
using MedicalSystem.API.Entities;
using MedicalSystem.API.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalSystem.API.Services
{
	public class ReceptionService : IReceptionService
	{
		private readonly ApplicationDbContext _context;
		public ReceptionService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<ReceptionService> logger)
		{
			_context = context;
		}

		public async Task<IEnumerable<ReceptionResponse>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var reception = await (from u in _context.Users
								  join ur in _context.UserRoles
								  on u.Id equals ur.UserId
								  join r in _context.Roles
								  on ur.RoleId equals r.Id
								  where r.Name == DefaultRoles.Reception && u.IsDisabled == false
								  select new ReceptionResponse
								  (
									 u.Id,
									 u.FullName,
									 u.CreatedAt,
									 u.PhoneNumber!,
									 r.Name!,
									 u.Email!
								  )).ToListAsync(cancellationToken);
			return reception;
		}
	}
}
