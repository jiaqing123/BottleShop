using Microsoft.Azure.Cosmos.Table;

namespace BottleShop.Storage.Entities
{
	/// <summary>
	/// Trolley representation in Table. 
	/// </summary>
	/// <remarks>
	/// PartitionKey: trolleyId
	/// 
	/// RowKey: category info
	/// __basic: basic info
	/// __promotion_uid: trolley id
	/// __product_uid: product id
	/// __product_promotion_uid: product promotion id
	/// </remarks>
	public class TrolleyTableEntity : TableEntity
	{
		/// <summary>
		/// Type of row
		/// </summary>
		public string RowType { get; set; }

		/// <summary>
		/// Child rows in json format
		/// </summary>
		public string Children { get; set; }

		/// <summary>
		/// For example, unit price of product
		/// </summary>
		public decimal? UnitPrice { get; set; }

		/// <summary>
		/// For example, quantity of product
		/// </summary>
		public decimal? Quantity { get; set; }

		/// <summary>
		/// List of serialized PromotionColumnEntity
		/// </summary>
		public string Promotions { get; set; }

		/// <summary>
		/// Total price
		/// </summary>
		public decimal? Total { get; set; }

		/// <summary>
		/// Total price after discount
		/// </summary>
		public decimal? DiscountedTotal { get; set; }
	}
}
