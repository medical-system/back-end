using MedicalSystem.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReceptionsController : ControllerBase
	{
		private readonly IReceptionService _receptionService;
		public ReceptionsController(IReceptionService receptionService)
		{
			_receptionService = receptionService;
		}
		[HttpGet("")]
		public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
		{
			return Ok(await _receptionService.GetAllAsync(cancellationToken));
		}
	}
}
