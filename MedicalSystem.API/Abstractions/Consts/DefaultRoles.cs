namespace MedicalSystem.API.Abstractions.Consts
{
	public static class DefaultRoles
	{
		public const string Admin = nameof(Admin);
		public const string AdminRoleId = "92b75286-d8f8-4061-9995-e6e23ccdee94";
		public const string AdminRoleConcurrencyStamp = "f51e5a91-bced-49c2-8b86-c2e170c0846c";

		public const string User = nameof(User);
		public const string UserRoleId = "9eaa03df-8e4f-4161-85de-0f6e5e30bfd4";
		public const string UserRoleConcurrencyStamp = "5ee6bc12-5cb0-4304-91e7-6a00744e042a";

		public const string Patient = nameof(Patient);
		public const string PatientRoleId = "35caa9a9-eddd-4022-8b1f-ead30fae196d";
		public const string PatientRoleConcurrencyStamp = "778589ec-2e7d-499a-8b51-96b6cfff8cd7";

		public const string Doctor = nameof(Doctor);
		public const string DoctorRoleId = "773f4c1b-da3f-40f3-bd41-08338332eab9";
		public const string DoctorRoleConcurrencyStamp = "c93c54e0-6f99-4d6e-8a4f-b9e2a3208163";

		public const string Reception = nameof(Reception);
		public const string ReceptionRoleId = "096c40db-bbd6-4f29-a643-e4f5c12ab0a9";
		public const string ReceptionRoleConcurrencyStamp = "1b153437-cb20-48b7-abe7-a55ddce20f95";
	}
}
