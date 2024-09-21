using DataAccessLayer.Models;
using PersistenceLayer.Repositories.ConcreteClasses;
using PersistenceLayer.Repositories.Interfaces;
using PersistenceLayer.UnitOfWork.Interfaces;
using System.Threading.Tasks;

namespace PersistenceLayer.UnitOfWork.ConcreteClasses
{
	public class UnitOfWork : IUnitOfWork
	{
		
		public UnitOfWork()
		{
			_marketContext = new MarketContext();
			OrderRepository = new OrderRepository(_marketContext);
			ProductRepository = new ProductRepository(_marketContext);
			SupplierRepository = new SupplierRepository(_marketContext);
			CustomerRepository = new CustomerRepository(_marketContext);
			EntryItemsRepository = new EntryItemsRepository(_marketContext);
			OrderItemsRepository = new OrderItemsRepository(_marketContext);
			ProductEntryRepository = new ProductEntryRepository(_marketContext);
			NotificationRepository = new NotificationRepository(_marketContext);
		}

		public void SaveChanges()
		{
			_marketContext.SaveChanges();
		}

		public void Dispose()
		{
			_marketContext.Dispose();
		}

		private readonly MarketContext _marketContext;

		public IOrderRepository OrderRepository { get; set; }

		public IProductRepository ProductRepository { get; set; }

		public ICustomerRepository CustomerRepository { get; set; }

		public ISupplierRepository SupplierRepository { get; set; }

		public IEntryItemsRepository EntryItemsRepository { get; set;}

		public IOrderItemsRepository OrderItemsRepository { get; set; }
		
		public IProductEntryRepository ProductEntryRepository { get; set; }

		public INotificationRepository NotificationRepository { get; set; }

	}
}
