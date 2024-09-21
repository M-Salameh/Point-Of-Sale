using ApplicationLayer.DTOs;
using ApplicationLayer.ServicesProvider;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Web.Models;

namespace Web.Controllers
{
    /// <summary>
    /// Customers GateWay
    /// </summary>
    
    [Authorize]
    public class CustomerController : BaseController
    {


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="servicePool"></param>
        public CustomerController(IServicePool servicePool) : base(servicePool) { }


        /// <summary>
        /// First Page Handler
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            IEnumerable<CustomerDTO> customers = servicePool.CustomerService.GetAllCustomers();
            return View(customers);
        }

        /// <summary>
        /// Add new Customer
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View("Views/Customer/Create.cshtml");
        }

        /// <summary>
        /// Insert the Added customer into the DB.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                var Id = servicePool.CustomerService.AddCustomerAndGetId(customer);

                TempData["Message"] = "The Information Is Added Successfully.";

                TempData["Success"] = "True";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                TempData["Message"] = "Sorry the The Add Process Failed.";

                TempData["Success"] = "False";
                return RedirectToAction(nameof(Index));
            }
        }


        /// <summary>
        /// Delete The information Of A specific Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                CustomerDTO customer = new CustomerDTO();
                customer.Id = id;

                servicePool.CustomerService.DeleteCustomer(customer);
                TempData["deleted"] = "True";
                TempData["Message"] = "The Customer Deleted Successfully.";
            }
            catch (Exception)
            {
                TempData["deleted"] = "False";
                TempData["Message"] = "Sorry the The Delete Process Failed.";
            }
            return RedirectToAction(nameof(Index));
        }


        /// <summary>
        /// Returns the customer for the form to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            CustomerDTO customerDTO = new CustomerDTO();

            try
            {
                customerDTO = servicePool.CustomerService.GetCustomerByID(id);
            }
            catch (Exception exception)
            {
                TempData["exception"] = exception.Message;
            }

            return View("Edit", customerDTO);
        }

        /// <summary>
        /// Upadte the information in the DB.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                servicePool.CustomerService.UpdateCustomer(customer.Id, customer);
                TempData["Success"] = "True";
                TempData["Message"] = "The Information Is Updated Successfully.";

            }
            else
            {
                TempData["Success"] = "False";
                TempData["Message"] = "Sorry the The Update Process Failed.";
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// get all matches 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetByName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return View("Views/Errors/500.cshtml");

            }
            IEnumerable<CustomerDTO> customers =
                servicePool.CustomerService.GetAllCustomersByFilter(
                    customer =>
                    customer.Name.ToLower().Contains(name.ToLower())
                    );
            TempData["search"] = "Suppliers Match The Name '  " + name + "  '";
            return View("Views/Customer/Index.cshtml", customers);
        }
    }
}
