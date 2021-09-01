using System;
using System.ComponentModel.DataAnnotations;

namespace BottleShop.Models
{
	public class ProductQuantityParameter
	{
		[Required]
		public string ProductId { get; set; }

		[Required]
		[Range(1, int.MaxValue)]
		public int Quantity { get; set; }
	}
}
