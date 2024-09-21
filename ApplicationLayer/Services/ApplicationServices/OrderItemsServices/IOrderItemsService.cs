using ApplicationLayer.DTOs;
using ApplicationLayer.Services.ApplicationServices.BaseServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.Services.ApplicationServices.OrderItemsServices
{
	public interface IOrderItemsService : IBaseService
	{
		#region Create

		void AddOrderItems(OrderItemsDTO orderItemsDTO);
		void AddOrderItemsAsync(OrderItemsDTO orderItemsDTO);
		void AddRangeOfOrderItemss(IEnumerable<OrderItemsDTO> orderItemssDTO);
		void AddRangeOfOrderItemssAsync(IEnumerable<OrderItemsDTO> orderItemssDTO);

		#endregion

		#region Read

		OrderItemsDTO GetOrderItemsByID<IDType>(IDType id);
		OrderItemsDTO GetOrderItemsByIDAsync<IDType>(IDType id);
		OrderItemsDTO GetOrderItemsByFilter(Func<OrderItemsDTO, bool> filter);
		IEnumerable<OrderItemsDTO> GetAllOrderItemss();
		IEnumerable<OrderItemsDTO> GetAllOrderItemssByFilter(Func<OrderItemsDTO, bool> filter);

		#endregion

		#region Update

		void UpdateOrderItems<IDType>(IDType ID, OrderItemsDTO newOrderItems);

		#endregion

		#region Delete

		void DeleteOrderItems(OrderItemsDTO orderItemsDTO);
		void DeleteOrderItemsById(int id);
		void DeleteRangeOfOrderItemss(IEnumerable<OrderItemsDTO> orderItemssDTO);

		#endregion

		#region Additional Functionalities

		bool Exist(int Id);

		#endregion
	}
}
