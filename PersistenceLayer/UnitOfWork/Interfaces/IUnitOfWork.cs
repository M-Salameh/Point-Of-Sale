using DataAccessLayer.Models;
using PersistenceLayer.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace PersistenceLayer.UnitOfWork.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		void SaveChanges();
		IOrderRepository OrderRepository { get; set; }
		IProductRepository ProductRepository { get; set; }
		ICustomerRepository CustomerRepository { get; set; }
		ISupplierRepository SupplierRepository { get; set; }
		IOrderItemsRepository OrderItemsRepository { get; set; }
		IEntryItemsRepository EntryItemsRepository { get; set; }
		IProductEntryRepository ProductEntryRepository { get; set; }
		INotificationRepository NotificationRepository { get; set; }

	}
}
