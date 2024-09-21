using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.ConcreteClasses
{
	public class OrderItemsRepository : BaseRepository<OrderItems>, IOrderItemsRepository
	{
		public OrderItemsRepository(MarketContext marketContext)
			: base(marketContext)
		{ }
	}
}
