using MedicalSystem.API.Abstractions;

namespace MedicalSystem.API.Errors
{
	public static class ServicessErrors
	{
		public static readonly Error DuplicatedName = new("Service.DuplicatedName", "Another Service with the same Name is already exists", StatusCodes.Status409Conflict);

		public static readonly Error ServiceNotFound = new("Service.ServiceNotFound", "Service is not found", StatusCodes.Status404NotFound);

	}
}
