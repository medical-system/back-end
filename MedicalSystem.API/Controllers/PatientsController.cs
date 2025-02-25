using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Contracts.cs.MedicalRecord;
using MedicalSystem.API.Contracts.cs.Patients;
using MedicalSystem.API.Extensions;
using MedicalSystem.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PatientsController : ControllerBase
	{
		private readonly IPatientService _patientService;
		public PatientsController(IPatientService patientService)
		{
			_patientService = patientService;
		}
		[HttpGet("")]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			return Ok(await _patientService.GetAllAsync(cancellationToken));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById([FromRoute] string id)
		{
			var result = await _patientService.GetAsync(id);
			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}

		[HttpPost("Add")]
		public async Task<IActionResult> Add([FromForm] CreatePatientRequest request, CancellationToken cancellationToken)
		{
			var result = await _patientService.AddAsync(User.GetUserId()! ,request, cancellationToken);

			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}

		[HttpGet("{id}/medical-record")]
		public async Task<IActionResult> MedicalRecord([FromRoute] string id)
		{
			return Ok(await _patientService.GetAllMedicalRecordAsync(id));
		}

		[HttpGet("count-patient")]
		public async Task<IActionResult> GetCountPatient(CancellationToken cancellationToken)
		{
			return Ok(await _patientService.GetTotalPatientsAsync(cancellationToken));
		}

		[HttpPost("{id}/medical-record")]
		public async Task<IActionResult> AddMedicalRecord([FromRoute] string id, [FromBody] CreateMedicalRecordRequest request, CancellationToken cancellationToken)
		{
			var result = await _patientService.AddPatientMedicalRecord(id, request, cancellationToken);
			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}
	}
}
