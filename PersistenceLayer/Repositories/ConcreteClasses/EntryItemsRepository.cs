using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.ConcreteClasses
{
	public class EntryItemsRepository : BaseRepository<EntryItems>, IEntryItemsRepository
	{
		public EntryItemsRepository(MarketContext marketContext)
			: base(marketContext)
		{ }
	}
}
