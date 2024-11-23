using MedicalSystem.API.Abstractions;
using MedicalSystem.API.Contracts.cs.Patients;
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
		public async Task<IActionResult> Add([FromBody] CreatePatientRequest request, CancellationToken cancellationToken)
		{
			var result = await _patientService.AddAsync(request, cancellationToken);

			return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { result.Value.Id }, result.Value) : result.ToProblem();
		}

		[HttpPut("{id}/toggle-status")]
		public async Task<IActionResult> ToggleStatus([FromRoute] string id)
		{
			var result = await _patientService.ToggleStatus(id);
			return result.IsSuccess ? NoContent() : result.ToProblem();
		}
	}
}
