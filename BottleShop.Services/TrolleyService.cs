using AutoMapper;
using BottleShop.Models;
using BottleShop.Storage;
using BottleShop.Storage.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BottleShop.Services
{
	public class TrolleyService : ITrolleyService
	{
		#region ITrolleyService Members

		public Task<Trolley> GetCustomerTrolleyAsync(string customerId)
		{
			var partitionKey = customerId;

			var entity = _trolleyRepository.GetItems(partitionKey, 1)
				.FirstOrDefault();

			var trolley = _mapper.Map<Trolley>(entity);

			return Task.FromResult(trolley);
		}

		#endregion

		private readonly IMapper _mapper;
		private readonly IRepository<TrolleyEntity> _trolleyRepository;

		public TrolleyService(IMapper mapper, IRepository<TrolleyEntity> trolleyRepository)
		{
			_mapper = mapper;
			_trolleyRepository = trolleyRepository;
		}
	}
}
