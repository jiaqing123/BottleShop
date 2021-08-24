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
		public Trolley Get()
		{
			return new Trolley()
			{
				Products = new List<Product>()
				{
					new Product()
					{
						Id = 1,
						Name = "Victoria Bitter",
						Price = 21.49m,
					},
					new Product()
					{
						Id = 2,
						Name = "Crown Lager",
						Price = 22.99m
					}
				}
			};
		}
	}
}