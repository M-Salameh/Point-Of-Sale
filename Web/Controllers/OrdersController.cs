using ApplicationLayer.DTOs;
using ApplicationLayer.ServicesProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;

namespace Web.Controllers
{
	[Authorize]
	public class OrdersController : BaseController
	{
		public OrdersController(IServicePool servicePool) : base(servicePool)
		{
		}

		public IActionResult Index()
		{
			return View();
		}


		[HttpPost]
		public IActionResult GetOrders(string startDate, string endDate)
		{
			if(startDate == null)
			{
				startDate = DateTime.Now.Date.ToString();
			}
			if(endDate == null)
			{
                endDate = DateTime.Now.Date.ToString("MM-dd-yyyyy");
            }
            endDate = endDate += ", 23:59:59";
            DateTime dateTimeStart = DateTime.Parse(startDate);
            DateTime dateTimeEnd = DateTime.Parse(endDate);


			List<OrderDTO> orders = servicePool.OrderService.FilterOrdersByDate(dateTimeStart,dateTimeEnd).ToList();

			
			return View("Views/Orders/AllByInterval.cshtml", orders);
		}



        [HttpPost]
        public IActionResult GetItems(int id)
        {
			OrderDTO order = servicePool.OrderService.GetAllInformation(id);
			return View("Views/Orders/GetItems.cshtml", order);
        }


        public IActionResult SelectCustomers()
        {
            return View("Views/Orders/SelectCustomers.cshtml");
        }



        [HttpPost]
        public ActionResult GetCustomersByName(string name)
        {
            IEnumerable<CustomerDTO> customers =
                servicePool.CustomerService.GetAllCustomersByFilter(
                    customer =>
                    customer.Name.ToLower().Contains(name.ToLower())
                    );
            ViewData["search Query"] = name;
            return View("Views/Orders/SelectFromCustomers.cshtml", customers);
        }


        [HttpPost]
        public ActionResult StartServing(int id)
        {
            List<OrderDTO> orders = servicePool.OrderService.GetAllOrdersByFilter(
                (order) =>
                order.CustomerId == id
            ).ToList();
            return View("Views/Orders/OrdersTable.cshtml", orders);
        }
    }
}
