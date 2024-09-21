using System;
using System.Linq;
using System.Collections.Generic;
using DataAccessLayer.Models;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.ConcreteClasses
{
	public class OrderRepository : BaseRepository<Order>, IOrderRepository
	{
		public OrderRepository(MarketContext marketContext)
			: base(marketContext)
		{ }

		public IEnumerable<Order> GetPage(int pageNumber, int recordsPerPage)
		{
			int offsetIndex = (pageNumber - 1) * recordsPerPage;
			return _marketContext.Set<Order>().OrderBy((product) => product.Id).Skip(offsetIndex).Take(recordsPerPage).ToList();
		}
	}
}
