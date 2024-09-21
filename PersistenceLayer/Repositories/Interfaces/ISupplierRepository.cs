using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.Repositories.Interfaces
{
	public interface ISupplierRepository : IBaseRepository<Supplier>
	{
		IEnumerable<Supplier> GetPage(int pageNumber, int recordsPerPage);
	}
}
