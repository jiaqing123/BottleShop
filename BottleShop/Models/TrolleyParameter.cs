using System.ComponentModel.DataAnnotations;

namespace BottleShop.Models
{
	public class TrolleyParameter
	{
		[Required]
		public string CustomerId { get; set; }

		[Required]
		public string TrolleyId { get; set; }
	}
}
