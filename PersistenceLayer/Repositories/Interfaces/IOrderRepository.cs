using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.Repositories.Interfaces
{
	public interface IOrderRepository : IBaseRepository<Order>
	{
		IEnumerable<Order> GetPage(int pageNumber, int recordsPerPage);
	}
}
