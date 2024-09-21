using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.Repositories.Interfaces
{
	public interface ICustomerRepository : IBaseRepository<Customer>
	{
		IEnumerable<Customer> GetPage(int pageNumber, int recordsPerPage);
	}
}
