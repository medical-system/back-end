using MedicalSystem.API.Abstractions;

namespace MedicalSystem.API.Errors
{
	public static class MedicineErrors
	{
		public static readonly Error DuplicatedName = new("Medicine.DuplicatedName", "Another Medicine with the same Name is already exists", StatusCodes.Status409Conflict);

		public static readonly Error MedicineNotFound = new("Medicine.MedicineNotFound", "Medicine is not found", StatusCodes.Status404NotFound);
	}
}
