using System;
using System.Linq;
using DataAccessLayer.Models;
using System.Collections.Generic;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.ConcreteClasses
{
	public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
	{
		public CustomerRepository(MarketContext marketContext)
			: base(marketContext)
		{

		}

		public IEnumerable<Customer> GetPage(int pageNumber, int recordsPerPage)
		{
			int offsetIndex = (pageNumber - 1) * recordsPerPage;
			return _marketContext.Set<Customer>().OrderBy((customer) => customer.Id).Skip(offsetIndex).Take(recordsPerPage).ToList();
		}
	}
}
