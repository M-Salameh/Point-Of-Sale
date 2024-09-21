using System;
using ApplicationLayer.DTOs;
using System.Collections.Generic;
using ApplicationLayer.Services.ApplicationServices.BaseServices;

namespace ApplicationLayer.Services.ApplicationServices.OrderServices
{
	public interface IOrderService : IBaseService
	{

        #region Create

        void AddOrder(OrderDTO orderDTO);
        void AddOrderAsync(OrderDTO orderDTO);
        void AddRangeOfOrders(IEnumerable<OrderDTO> ordersDTO);
        void AddRangeOfOrdersAsync(IEnumerable<OrderDTO> ordersDTO);

        #endregion

        #region Read

        OrderDTO GetOrderByID<IDType>(IDType id);
        OrderDTO GetOrderByIDAsync<IDType>(IDType id);
        OrderDTO GetOrderByFilter(Func<OrderDTO, bool> filter);
        IEnumerable<OrderDTO> GetAllOrders();
        IEnumerable<OrderDTO> GetAllOrdersByFilter(Func<OrderDTO, bool> filter);

        #endregion

        #region Update

        void UpdateOrder<IDType>(IDType ID, OrderDTO newOrder);

        #endregion

        #region Delete

        void DeleteOrder(OrderDTO orderDTO);
        void DeleteOrderById(int id);

        void DeleteRangeOfOrders(IEnumerable<OrderDTO> ordersDTO);

        #endregion

        #region Additional Functionalities

        bool Exist(int Id);

        IEnumerable<OrderDTO> FilterOrdersByDate(DateTime startDate, DateTime endDate);

        OrderDTO GetAllInformation(int orderId);

        #endregion

    }
}
