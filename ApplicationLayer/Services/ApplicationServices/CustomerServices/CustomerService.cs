using System;
using ApplicationLayer.DTOs;
using DataAccessLayer.Models;
using System.Collections.Generic;
using ApplicationLayer.AutoMapper;
using PersistenceLayer.UnitOfWork.Interfaces;
using ApplicationLayer.Services.ApplicationServices.BaseServices;
using ApplicationLayer.Services.ApplicationServices.NotificationServices;

namespace ApplicationLayer.Services.ApplicationServices.CustomerServices
{
	public class CustomerService : BaseService , ICustomerService
	{

		public IAutomatedNotificationGenerator automatedNotificationGenerator { get; set; }

		public static AutoMapper<Customer, CustomerDTO> autoMapper;
		public CustomerService(IUnitOfWork unitOfWork, IAutomatedNotificationGenerator automatedNotificationGenerator)
			: base(unitOfWork)
		{
			this.automatedNotificationGenerator = automatedNotificationGenerator;
			autoMapper = new AutoMapper<Customer, CustomerDTO>();
		}

		#region Create

		public void AddCustomer(CustomerDTO customerDTO)
		{
			Customer customer = autoMapper.Map(customerDTO);
			customer.CreatedAt = DateTime.Now;
			customer.UpdatedAt = DateTime.Now;
			unitOfWork.CustomerRepository.Add(customer);
			unitOfWork.SaveChanges();
		}

		public void AddCustomerAsync(CustomerDTO customerDTO)
		{
			Customer customer = autoMapper.Map(customerDTO);
			customer.CreatedAt = DateTime.Now;
			customer.UpdatedAt = DateTime.Now;
			unitOfWork.CustomerRepository.AddAsync(customer);
			unitOfWork.SaveChanges();
		}

		public void AddRangeOfCustomers(IEnumerable<CustomerDTO> customersDTO)
		{
			List<Customer> customers = new List<Customer>();

			foreach(CustomerDTO customerDTO in customersDTO)
			{
				customerDTO.CreatedAt = DateTime.Now;
				customerDTO.UpdatedAt = DateTime.Now;
				customers.Add(autoMapper.Map(customerDTO));
			}
			unitOfWork.CustomerRepository.AddRange(customers);
			unitOfWork.SaveChanges();
		}

		public void AddRangeOfCustomersAsync(IEnumerable<CustomerDTO> customersDTO)
		{
			List<Customer> customers = new List<Customer>();

			foreach (CustomerDTO customerDTO in customersDTO)
			{
				customerDTO.CreatedAt = DateTime.Now;
				customerDTO.UpdatedAt = DateTime.Now;
				customers.Add(autoMapper.Map(customerDTO));
			}
			unitOfWork.CustomerRepository.AddRangeAsync(customers);
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Read

		public CustomerDTO GetCustomerByID<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.CustomerRepository.GetByID(id));
		}

		public CustomerDTO GetCustomerByIDAsync<IDType>(IDType id)
		{
			return autoMapper.Map(unitOfWork.CustomerRepository.GetByIDAsync(id).Result);
		}

		public CustomerDTO GetCustomerByFilter(Func<CustomerDTO, bool> filter)
		{
			Func<Customer, bool> AdaptiveFilter = (customer) => filter(autoMapper.Map(customer));

			return autoMapper.Map(unitOfWork.CustomerRepository.GetByFilter(AdaptiveFilter));
		}

		public IEnumerable<CustomerDTO> GetAllCustomers()
		{
			IEnumerable<Customer> customers = unitOfWork.CustomerRepository.GetAll();

			List<CustomerDTO> customersDTO = new List<CustomerDTO>();

			foreach(Customer customer in customers)
			{
				customersDTO.Add(autoMapper.Map(customer));
			}

			return customersDTO;
		}

		public IEnumerable<CustomerDTO> GetAllCustomersByFilter(Func<CustomerDTO, bool> filter)
		{

			Func<Customer, bool> AdaptiveFilter = (customer) => filter(autoMapper.Map(customer));

			IEnumerable<Customer> customers = unitOfWork.CustomerRepository.GetAllByFilter(AdaptiveFilter);

			List<CustomerDTO> customersDTO = new List<CustomerDTO>();

			foreach (Customer customer in customers)
			{
				customersDTO.Add(autoMapper.Map(customer));
			}

			return customersDTO;
		}

		#endregion

		#region Update

		public void UpdateCustomer<IDType>(IDType ID, CustomerDTO newCustomerDTO)
		{
			newCustomerDTO.UpdatedAt = DateTime.Now;
			unitOfWork.CustomerRepository.Update(ID, autoMapper.Map(newCustomerDTO));
			unitOfWork.SaveChanges();
		}

		#endregion

		#region Delete

		public void DeleteCustomer(CustomerDTO customerDTO)
		{
			unitOfWork.CustomerRepository.Delete(autoMapper.Map(customerDTO));
			unitOfWork.SaveChanges();
		}

		public void DeleteRangeOfCustomers(IEnumerable<CustomerDTO> customersDTO)
		{
			List<Customer> customers = new List<Customer>();

			foreach (CustomerDTO customerDTO in customersDTO)
			{
				customers.Add(autoMapper.Map(customerDTO));
			}
			unitOfWork.CustomerRepository.DeleteRange(customers);
			unitOfWork.SaveChanges();
		}

		public void DeleteCustomerById(int id)
		{

			Func<Order , bool> OrderFilter = 
				(order) => order.CustomerId == id;

			Func<OrderItems, bool> OrderItemsFilter =
				(orderitems) => orderitems.OrderId == id;

			IEnumerable<Order> orders = unitOfWork.OrderRepository.GetAllByFilter(OrderFilter);

			IEnumerable<OrderItems> orderItems = new List<OrderItems>();

			foreach (Order order in orders)
            {
				orderItems = unitOfWork.OrderItemsRepository.GetAllByFilter(OrderItemsFilter);
				
				unitOfWork.OrderItemsRepository.DeleteRange(orderItems);
			}

			unitOfWork.OrderRepository.DeleteRange(orders);

			unitOfWork.CustomerRepository.DeleteById(id);

			unitOfWork.SaveChanges();
		}

		#endregion

		#region Additional Functionalities

		public bool Exist(int Id)
		{
			return unitOfWork.CustomerRepository.Exist((customer) => customer.Id == Id);
		}

		public void AddOrder(int customerId, IEnumerable<ProductDTO> productDTOs)
		{
			int totalPrice = 0;

			foreach(ProductDTO productDTO in productDTOs)
            {
				totalPrice += productDTO.Price * productDTO.Quantity;
			}				

			Order order = new Order 
			{ 
				CustomerId = customerId,
				TotalPrice = totalPrice, 
				CreatedAt = DateTime.Now, 
				UpdatedAt = DateTime.Now 
			};

			unitOfWork.OrderRepository.Add(order);
			unitOfWork.SaveChanges();

			List<OrderItems> orderItems = new List<OrderItems>();

			// Seperation of two Loops to maintain DB integrity

			// Loop 1
			foreach (ProductDTO productDTO in productDTOs)
			{
				orderItems.Add
					(
						new OrderItems
						{
							OrderId = order.Id,
							ProductId = productDTO.Id,
							Price = productDTO.Price * productDTO.Quantity,
							Quantity = productDTO.Quantity,
							CreatedAt = DateTime.Now,
							UpdatedAt = DateTime.Now
						}
					);
			}

			unitOfWork.OrderItemsRepository.AddRange(orderItems);

			// Loop 2
			Product product = new Product();
			foreach (ProductDTO productDTO in productDTOs)
			{
				product = unitOfWork.ProductRepository.GetByFilter((product) => product.Id == productDTO.Id);
				product.Quantity -= productDTO.Quantity;
				product.UpdatedAt = DateTime.Now;

				if (product.Quantity <= product.AlertQuantity)
                {
					automatedNotificationGenerator.SendAutomatedNotification
					(
						new NotificationMessage
						{
							MessageSubject = "Product " + product.Name + " Quantity Alert",
							MessageContent = "We are running low on this product, PLEASE CONTACT YOUR SUPPLIERS ",
							BroadCastedAt = DateTime.Now
						}
					);
                }
				
				unitOfWork.ProductRepository.Update<int>(productDTO.Id, product);
				unitOfWork.SaveChanges();
			}

			unitOfWork.SaveChanges();

		}

		public int AddCustomerAndGetId(CustomerDTO customerDTO)
		{

			Customer customer = autoMapper.Map(customerDTO);
			customer.CreatedAt = DateTime.Now;
			customer.UpdatedAt = DateTime.Now;
			unitOfWork.CustomerRepository.Add(customer);
			unitOfWork.SaveChanges();
			return customer.Id;
		
		}

		#endregion

	}
}