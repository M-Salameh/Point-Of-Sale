using System.Linq;
using DataAccessLayer.Models;
using System.Collections.Generic;
using PersistenceLayer.Repositories.Interfaces;
using System;

namespace PersistenceLayer.Repositories.ConcreteClasses
{
	public class ProductRepository : BaseRepository<Product>, IProductRepository
	{
		public ProductRepository(MarketContext marketContext)
			: base(marketContext)
		{ }

		public IEnumerable<Product> GetPage(int pageNumber, int recordsPerPage)
		{
			int offsetIndex= (pageNumber - 1) * recordsPerPage;
			return _marketContext.Set<Product>().OrderByDescending((product) => product.Id).Skip(offsetIndex).Take(recordsPerPage).ToList();
		}

		public IEnumerable<Product> GetProductsById(IEnumerable<int> Identifiers)
		{
			Func<Product, bool> filter = (product) => Identifiers.Contains(product.Id);
			return GetAllByFilter(filter);
		}
	}
}
