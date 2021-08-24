using BottleShop.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottleShop.Storage
{
	public class DocumentRepository<T> : IRepository<T> where T : class
	{
		#region IDocumentRepository Members

		public T GetItem(string id, string partitionKey)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> GetItems(string partitionKey, int count = -1)
		{
			return Enumerable.Repeat(new TrolleyEntity()
			{
				Products = new List<ProductEntity>()
				{
					new ProductEntity()
					{
						Id = 1,
						Name = "Victoria Bitter",
						Price = 21.49m,
					},
					new ProductEntity()
					{
						Id = 2,
						Name = "Crown Lager",
						Price = 22.99m
					}
				}
			}, 1).Cast<T>();
		}

		#endregion
	}
}
