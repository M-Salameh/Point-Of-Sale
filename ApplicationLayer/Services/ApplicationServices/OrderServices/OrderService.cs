using System;
using ApplicationLayer.DTOs;
using DataAccessLayer.Models;
using System.Collections.Generic;
using ApplicationLayer.AutoMapper;
using PersistenceLayer.UnitOfWork.Interfaces;
using ApplicationLayer.Services.ApplicationServices.BaseServices;

namespace ApplicationLayer.Services.ApplicationServices.OrderServices
{
	public class OrderService : BaseService , IOrderService
	{
		public static AutoMapper<Order, OrderDTO> autoMapper;

		public OrderService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			autoMapper = new AutoMapper<Order, OrderDTO>();
		}

		#region Create

		public void AddOrder(OrderDTO orderDTO)
		{
			if (!unitOfWork.CustomerRepository.Exist((customer) => customer.Id == orderDTO.CustomerId))
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}
			
			Order order = autoMapper.Map(orderDTO);
			order.CreatedAt = DateTime.Now;
			order.UpdatedAt = DateTime.Now;

			unitOfWork.OrderRepository.Add(order);
			unitOfWork.SaveChanges();
			
		}

		public void AddOrderAsync(OrderDTO orderDTO)
		{
			if (unitOfWork.CustomerRepository.Exist((customer) => customer.Id == orderDTO.CustomerId))
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}
			
			Order order = autoMapper.Map(orderDTO);
			order.CreatedAt = DateTime.Now;
			order.UpdatedAt = DateTime.Now;

			unitOfWork.OrderRepository.AddAsync(order);
			unitOfWork.SaveChanges();
			
		}

		public void AddRangeOfOrders(IEnumerable<OrderDTO> ordersDTO)
		{
			List<Order> orders = new List<Order>();

			foreach (OrderDTO orderDTO in ordersDTO)
			{
				if (unitOfWork.CustomerRepository.Exist((customer) => customer.Id == orderDTO.CustomerId))
				{
					orderDTO.CreatedAt = DateTime.Now;
					orderDTO.UpdatedAt = DateTime.Now;
					orders.Add(autoMapper.Map(orderDTO));
				}
			}

			if (orders.Count != 0)
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}
			
			unitOfWork.OrderRepository.AddRange(orders);
			unitOfWork.SaveChanges();

		}

		public void AddRangeOfOrdersAsync(IEnumerable<OrderDTO> ordersDTO)
		{
			List<Order> orders = new List<Order>();

			foreach (OrderDTO orderDTO in ordersDTO)
			{
				if (unitOfWork.CustomerRepository.Exist((customer) => customer.Id == orderDTO.CustomerId))
				{
					orderDTO.CreatedAt = DateTime.Now;
					orderDTO.UpdatedAt = DateTime.Now;
					orders.Add(autoMapper.Map(orderDTO));
				}
			}

			if (orders.Count != 0)
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}

			unitOfWork.OrderRepository.AddRangeAsync(orders);
			unitOfWork.SaveChanges();

		}

		#endregion

		#region Read

		public OrderDTO GetOrderByID<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.OrderRepository.GetByID(id));
		}

		public OrderDTO GetOrderByIDAsync<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.OrderRepository.GetByIDAsync(id).Result);
		}

		public OrderDTO GetOrderByFilter(Func<OrderDTO, bool> filter)
		{
			Func<Order, bool> AdaptiveFilter = (order) => filter(autoMapper.Map(order));

			return autoMapper.Map(unitOfWork.OrderRepository.GetByFilter(AdaptiveFilter));
		}

		public IEnumerable<OrderDTO> GetAllOrders()
		{
			IEnumerable<Order> orders = unitOfWork.OrderRepository.GetAll();

			List<OrderDTO> ordersDTO = new List<OrderDTO>();

			foreach (Order order in orders)
			{
				ordersDTO.Add(autoMapper.Map(order));
			}

			return ordersDTO;
		}

		public IEnumerable<OrderDTO> GetAllOrdersByFilter(Func<OrderDTO, bool> filter)
		{

			Func<Order, bool> AdaptiveFilter = (order) => filter(autoMapper.Map(order));

			IEnumerable<Order> orders = unitOfWork.OrderRepository.GetAllByFilter(AdaptiveFilter);

			List<OrderDTO> ordersDTO = new List<OrderDTO>();

			foreach (Order order in orders)
			{
				ordersDTO.Add(autoMapper.Map(order));
			}

			return ordersDTO;
		}

		#endregion

		#region Update

		public void UpdateOrder<IDType>(IDType ID, OrderDTO newOrderDTO)
		{
			if (!unitOfWork.CustomerRepository.Exist((customer) => customer.Id == newOrderDTO.CustomerId))
			{
				throw new MissingMemberException("There was no Customer with the given Id");
			}

			newOrderDTO.UpdatedAt = DateTime.Now;
			unitOfWork.OrderRepository.Update(ID, autoMapper.Map(newOrderDTO));
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Delete

		public void DeleteOrder(OrderDTO orderDTO)
		{
			unitOfWork.OrderRepository.Delete(autoMapper.Map(orderDTO));
			unitOfWork.SaveChanges();
		}

		public void DeleteRangeOfOrders(IEnumerable<OrderDTO> ordersDTO)
		{
			List<Order> orders = new List<Order>();

			foreach (OrderDTO orderDTO in ordersDTO)
			{
				orders.Add(autoMapper.Map(orderDTO));
			}
			unitOfWork.OrderRepository.DeleteRange(orders);
			unitOfWork.SaveChanges();
		}

		public void DeleteOrderById(int id)
		{
			unitOfWork.OrderRepository.DeleteById(id);
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Additional Functionalities

		public bool Exist(int Id)
		{
			return unitOfWork.OrderRepository.Exist((order) => order.Id == Id);
		}

		public IEnumerable<OrderDTO> FilterOrdersByDate(DateTime startDate, DateTime endDate)
        {
			IEnumerable<OrderDTO> orders = GetAllOrdersByFilter
			(
				(order) => order.CreatedAt >= startDate && order.CreatedAt <= endDate
			);

			int customerId;

			Customer customer = new Customer();

			foreach(OrderDTO order in orders)
            {
				customerId = (int) order.CustomerId;
				customer = unitOfWork.CustomerRepository.GetByID<int>(customerId);

				order.CustomerDTO = CustomerServices.CustomerService.autoMapper.Map(customer);
				
			}

			return orders;
        }

		public OrderDTO GetAllInformation(int orderId)
        {
			IEnumerable<OrderItems> orderItems = unitOfWork.OrderItemsRepository.GetAllByFilter((orderItems) => orderItems.OrderId == orderId);

			List<OrderItemsDTO> orderItemsDTOs = new List<OrderItemsDTO>();
			
			foreach (OrderItems orderItem in orderItems)
            {
				orderItemsDTOs.Add(OrderItemsServices.OrderItemsService.autoMapper.Map(orderItem));
			}


			Product product = new Product();
			int productId;

			foreach (OrderItemsDTO orderItemsDTO in orderItemsDTOs)
			{
				productId = (int) orderItemsDTO.ProductId;
				product = unitOfWork.ProductRepository.GetByID<int>(productId);

				orderItemsDTO.Product = ProductServices.ProductService.autoMapper.Map(product);

			}

			Order order = unitOfWork.OrderRepository.GetByID<int>(orderId);
			OrderDTO orderDTO = autoMapper.Map(order);
			orderDTO.OrderItems = orderItemsDTOs;

			return orderDTO;
        }

		#endregion

	}
}