using BottleShop.Models;
using BottleShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BottleShop.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TrolleyController : ControllerBase
	{
		private readonly ITrolleyService _trolleyService;

		public TrolleyController(ITrolleyService trolleyService)
		{
			_trolleyService = trolleyService;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] TrolleyParameter trolley)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			// use not found exception
			var result = await _trolleyService.GetTrolleyAsync(trolley.TrolleyId);

			if (result == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(result);
			}
		}

		[HttpPost]
		public async Task<IActionResult> IncreaseProductQuantity([FromQuery] TrolleyParameter trolley, [FromBody] ProductQuantityParameter productQuantity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _trolleyService.IncreaseProductQuantityAsync(trolley.CustomerId, trolley.TrolleyId, productQuantity.ProductId, productQuantity.Quantity);

			if (result == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(result);
			}
		}

		[HttpPut]
		public async Task<IActionResult> ReduceProductQuantity([FromQuery] TrolleyParameter trolleyQuery, [FromBody] ProductQuantityParameter productQuantity)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await _trolleyService.DecreaseProductQuantityAsync(trolleyQuery.CustomerId, trolleyQuery.TrolleyId, productQuantity.ProductId, productQuantity.Quantity);

			if (result == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(result);
			}
		}
	}
}