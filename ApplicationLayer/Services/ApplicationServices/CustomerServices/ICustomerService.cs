using System;
using ApplicationLayer.DTOs;
using System.Collections.Generic;
using ApplicationLayer.Services.ApplicationServices.BaseServices;
using DataAccessLayer.Models;

namespace ApplicationLayer.Services.ApplicationServices.CustomerServices
{
	public interface ICustomerService : IBaseService
	{

		#region Create

		void AddCustomerAsync(CustomerDTO customerDTO);
		void AddCustomer(CustomerDTO customerDTO);
		void AddRangeOfCustomers(IEnumerable<CustomerDTO> customersDTO);
		void AddRangeOfCustomersAsync(IEnumerable<CustomerDTO> customersDTO);

		#endregion

		#region Read

		CustomerDTO GetCustomerByID<IDType>(IDType id);
		CustomerDTO GetCustomerByIDAsync<IDType>(IDType id);
		CustomerDTO GetCustomerByFilter(Func<CustomerDTO, bool> filter);
		IEnumerable<CustomerDTO> GetAllCustomers();
		IEnumerable<CustomerDTO> GetAllCustomersByFilter(Func<CustomerDTO, bool> filter);

		#endregion

		#region Update
		public void UpdateCustomer<IDType>(IDType ID, CustomerDTO newCustomer);

		#endregion

		#region Delete
		void DeleteCustomer(CustomerDTO customerDTO);
		void DeleteCustomerById(int id);

		void DeleteRangeOfCustomers(IEnumerable<CustomerDTO> customersDTO);

		#endregion

		#region Additional Functionalities

		bool Exist(int Id);
		void AddOrder(int customerId, IEnumerable<ProductDTO> productDTOs);
		int AddCustomerAndGetId(CustomerDTO customerDTO);

		#endregion

	}
}