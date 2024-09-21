using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.ConcreteClasses
{
	public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
	{
		public SupplierRepository(MarketContext marketContext)
			: base(marketContext)
		{
			
		}

		public IEnumerable<Supplier> GetPage(int pageNumber, int recordsPerPage)
		{
			int offsetIndex = (pageNumber - 1) * recordsPerPage;
			return _marketContext.Set<Supplier>().OrderBy((supplier) => supplier.Id).Skip(offsetIndex).Take(recordsPerPage).ToList();
		}

	}
}
