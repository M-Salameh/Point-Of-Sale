using ApplicationLayer.Services.ApplicationServices.OrderServices;
using ApplicationLayer.Services.ApplicationServices.ProductServices;
using ApplicationLayer.Services.ApplicationServices.CustomerServices;
using ApplicationLayer.Services.ApplicationServices.SupplierServices;
using ApplicationLayer.Services.ApplicationServices.EntryItemsServices;
using ApplicationLayer.Services.ApplicationServices.OrderItemsServices;
using ApplicationLayer.Services.ApplicationServices.ProductEntryServices;
using ApplicationLayer.Services.ApplicationServices.NotificationServices;

namespace ApplicationLayer.ServicesProvider
{
    public interface IServicePool
    {

        public IOrderService OrderService { get; set; }
        public IProductService ProductService { get; set; }
        public ICustomerService CustomerService { get; set; }
        public ISupplierService SupplierService { get; set; }
        public IEntryItemsService EntryItemsService { get; set; }
        public IOrderItemsService OrderItemsService { get; set; }
        public IProductEntryService ProductEntryService { get; set; }
        public INotificationService NotificationService { get; set; } 
       

    }
}
