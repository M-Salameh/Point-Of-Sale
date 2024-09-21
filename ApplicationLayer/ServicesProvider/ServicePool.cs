using PersistenceLayer.UnitOfWork.Interfaces;
using ApplicationLayer.Services.ApplicationServices.OrderServices;
using ApplicationLayer.Services.ApplicationServices.ProductServices;
using ApplicationLayer.Services.ApplicationServices.CustomerServices;
using ApplicationLayer.Services.ApplicationServices.SupplierServices;
using ApplicationLayer.Services.ApplicationServices.EntryItemsServices;
using ApplicationLayer.Services.ApplicationServices.OrderItemsServices;
using ApplicationLayer.Services.ApplicationServices.ProductEntryServices;
using ApplicationLayer.Services.ApplicationServices.NotificationServices;
using Microsoft.AspNetCore.SignalR;

namespace ApplicationLayer.ServicesProvider
{
	public class ServicePool : IServicePool
	{
		public IUnitOfWork unitOfWork;

		public ServicePool(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext, IAutomatedNotificationGenerator automatedNotificationGenerator)
		{
			this.unitOfWork = unitOfWork;
			OrderService = new OrderService(unitOfWork);
			ProductService = new ProductService(unitOfWork);
			CustomerService = new CustomerService(unitOfWork, automatedNotificationGenerator);
			SupplierService = new SupplierService(unitOfWork);
			EntryItemsService = new EntryItemsService(unitOfWork);
			OrderItemsService = new OrderItemsService(unitOfWork);
			ProductEntryService = new ProductEntryService(unitOfWork);
			NotificationService = new NotificationService(unitOfWork, hubContext);
		}

		public IOrderService OrderService { get; set; }

		public IProductService ProductService { get; set; }

		public ICustomerService CustomerService { get; set; }

		public ISupplierService SupplierService { get; set; }

		public IEntryItemsService EntryItemsService { get; set; }

		public IOrderItemsService OrderItemsService { get ; set ; }

		public IProductEntryService ProductEntryService { get; set; }

		public INotificationService NotificationService { get; set; }

	}
}