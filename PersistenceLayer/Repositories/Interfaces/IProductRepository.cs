using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceLayer.Repositories.Interfaces
{
	public interface IProductRepository : IBaseRepository<Product>
	{
		IEnumerable<Product> GetPage(int pageNumber, int recordsPerPage);

		IEnumerable<Product> GetProductsById(IEnumerable<int> Identifiers);

	}
}
