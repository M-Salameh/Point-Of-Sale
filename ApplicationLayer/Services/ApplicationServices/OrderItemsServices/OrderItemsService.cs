using System;
using ApplicationLayer.DTOs;
using DataAccessLayer.Models;
using System.Collections.Generic;
using ApplicationLayer.AutoMapper;
using PersistenceLayer.UnitOfWork.Interfaces;
using ApplicationLayer.Services.ApplicationServices.BaseServices;

namespace ApplicationLayer.Services.ApplicationServices.OrderItemsServices
{
	public class OrderItemsService : BaseService, IOrderItemsService
	{
		public static AutoMapper<OrderItems, OrderItemsDTO> autoMapper;

		public OrderItemsService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
			autoMapper = new AutoMapper<OrderItems, OrderItemsDTO>();
		}

		#region Create

		public void AddOrderItems(OrderItemsDTO orderItemsDTO)
		{
			if (!unitOfWork.OrderRepository.Exist((order) => order.Id == orderItemsDTO.OrderId))
			{
				throw new MissingMemberException("There was no order with the given Id");
			}

			OrderItems orderItems = autoMapper.Map(orderItemsDTO);
			orderItems.CreatedAt = DateTime.Now;
			orderItems.UpdatedAt = DateTime.Now;

			unitOfWork.OrderItemsRepository.Add(orderItems);
			unitOfWork.SaveChanges();

		}

		public void AddOrderItemsAsync(OrderItemsDTO orderItemsDTO)
		{
			if (!unitOfWork.OrderRepository.Exist((order) => order.Id == orderItemsDTO.OrderId))
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}

			OrderItems orderItems = autoMapper.Map(orderItemsDTO);
			orderItems.CreatedAt = DateTime.Now;
			orderItems.UpdatedAt = DateTime.Now;

			unitOfWork.OrderItemsRepository.AddAsync(orderItems);
			unitOfWork.SaveChanges();

		}

		public void AddRangeOfOrderItemss(IEnumerable<OrderItemsDTO> orderItemssDTO)
		{
			List<OrderItems> orderItemss = new List<OrderItems>();

			foreach (OrderItemsDTO orderItemsDTO in orderItemssDTO)
			{
				if (unitOfWork.OrderRepository.Exist((order) => order.Id == orderItemsDTO.OrderId))
				{
					orderItemsDTO.CreatedAt = DateTime.Now;
					orderItemsDTO.UpdatedAt = DateTime.Now;
					orderItemss.Add(autoMapper.Map(orderItemsDTO));
				}
			}

			if (orderItemss.Count != 0)
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}

			unitOfWork.OrderItemsRepository.AddRange(orderItemss);
			unitOfWork.SaveChanges();

		}

		public void AddRangeOfOrderItemssAsync(IEnumerable<OrderItemsDTO> orderItemssDTO)
		{
			List<OrderItems> orderItemss = new List<OrderItems>();

			foreach (OrderItemsDTO orderItemsDTO in orderItemssDTO)
			{
				if (unitOfWork.OrderRepository.Exist((order) => order.Id == orderItemsDTO.OrderId))
				{
					orderItemsDTO.CreatedAt = DateTime.Now;
					orderItemsDTO.UpdatedAt = DateTime.Now;
					orderItemss.Add(autoMapper.Map(orderItemsDTO));
				}
			}

			if (orderItemss.Count != 0)
			{
				throw new MissingMemberException("There was no Supplier with the given Id");
			}

			unitOfWork.OrderItemsRepository.AddRangeAsync(orderItemss);
			unitOfWork.SaveChanges();

		}

		#endregion

		#region Read

		public OrderItemsDTO GetOrderItemsByID<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.OrderItemsRepository.GetByID(id));
		}

		public OrderItemsDTO GetOrderItemsByIDAsync<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.OrderItemsRepository.GetByIDAsync(id).Result);
		}

		public OrderItemsDTO GetOrderItemsByFilter(Func<OrderItemsDTO, bool> filter)
		{
			Func<OrderItems, bool> AdaptiveFilter = (orderItems) => filter(autoMapper.Map(orderItems));

			return autoMapper.Map(unitOfWork.OrderItemsRepository.GetByFilter(AdaptiveFilter));
		}

		public IEnumerable<OrderItemsDTO> GetAllOrderItemss()
		{
			IEnumerable<OrderItems> orderItemss = unitOfWork.OrderItemsRepository.GetAll();

			List<OrderItemsDTO> orderItemssDTO = new List<OrderItemsDTO>();

			foreach (OrderItems orderItems in orderItemss)
			{
				orderItemssDTO.Add(autoMapper.Map(orderItems));
			}

			return orderItemssDTO;
		}

		public IEnumerable<OrderItemsDTO> GetAllOrderItemssByFilter(Func<OrderItemsDTO, bool> filter)
		{

			Func<OrderItems, bool> AdaptiveFilter = (orderItems) => filter(autoMapper.Map(orderItems));

			IEnumerable<OrderItems> orderItemss = unitOfWork.OrderItemsRepository.GetAllByFilter(AdaptiveFilter);

			List<OrderItemsDTO> orderItemssDTO = new List<OrderItemsDTO>();

			foreach (OrderItems orderItems in orderItemss)
			{
				orderItemssDTO.Add(autoMapper.Map(orderItems));
			}

			return orderItemssDTO;
		}

		#endregion

		#region Update

		public void UpdateOrderItems<IDType>(IDType ID, OrderItemsDTO newOrderItemsDTO)
		{

			if (!unitOfWork.OrderRepository.Exist((order) => order.Id == newOrderItemsDTO.OrderId))
			{
				throw new MissingMemberException("There was no Customer with the given Id");
			}

			newOrderItemsDTO.UpdatedAt = DateTime.Now;
			unitOfWork.OrderItemsRepository.Update(ID, autoMapper.Map(newOrderItemsDTO));
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Delete

		public void DeleteOrderItems(OrderItemsDTO orderItemsDTO)
		{
			unitOfWork.OrderItemsRepository.Delete(autoMapper.Map(orderItemsDTO));
			unitOfWork.SaveChanges();
		}

		public void DeleteRangeOfOrderItemss(IEnumerable<OrderItemsDTO> orderItemssDTO)
		{
			List<OrderItems> orderItemss = new List<OrderItems>();

			foreach (OrderItemsDTO orderItemsDTO in orderItemssDTO)
			{
				orderItemss.Add(autoMapper.Map(orderItemsDTO));
			}

			unitOfWork.OrderItemsRepository.DeleteRange(orderItemss);
			unitOfWork.SaveChanges();
		}

		public void DeleteOrderItemsById(int id)
		{
			unitOfWork.OrderItemsRepository.DeleteById(id);
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Additional Functionalities

		public bool Exist(int Id)
		{
			return unitOfWork.OrderItemsRepository.Exist((orderItems) => orderItems.Id == Id);
		}

		#endregion

	}
}
