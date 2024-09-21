using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.ConcreteClasses
{
	public class ProductEntryRepository : BaseRepository<ProductEntry>, IProductEntryRepository
	{
		public ProductEntryRepository(MarketContext marketContext) : base(marketContext)
		{ }
	}
}
