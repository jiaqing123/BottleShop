using BottleShop.Models;
using BottleShop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public async Task<Trolley> Get()
		{
			var customerId = string.Empty;

			var result = await _trolleyService.GetCustomerTrolleyAsync(customerId);

			return result;
		}
	}
}