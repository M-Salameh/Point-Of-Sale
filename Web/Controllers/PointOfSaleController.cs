using ApplicationLayer.DTOs;
using ApplicationLayer.ServicesProvider;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;

namespace Web.Controllers
{
    /// <summary>
    /// The Handler Of The Point Of Sale Events.
    /// </summary>
    
    [Authorize]
    public class PointOfSaleController : BaseController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="servicePool"></param>
        public PointOfSaleController(IServicePool servicePool) : base(servicePool) { }


        /// <summary>
        /// 
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
            return View("Views/PointOfSale/CreateCustomer.cshtml");
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


                TempData["Success"] = "True";
                TempData["CustomerName"] = customer.Name;
                TempData["CustomerId"] = Id;

                return RedirectToAction(nameof(StartServingPagination), new RouteValueDictionary(new
                {
                    Controller = "PointOfSale",
                    Action = "StartServingPagination",
                    id = Id,

                }
              ));
            }
            else
            {
                TempData["Success"] = "False";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public ActionResult GetProductsByName(string name, int id)
        {
            if (string.IsNullOrEmpty(name))
            {
                return View("Views/Errors/500.cshtml");

            }
            IEnumerable<ProductDTO> products =
                servicePool.ProductService.GetAllProductsByFilter(
                    product =>
                    product.Name.ToLower().Contains(name.ToLower())
                    );

            PointOfSaleViewModel pointOfSaleViewModel = new PointOfSaleViewModel();
            pointOfSaleViewModel.PageNumber = 1;
            pointOfSaleViewModel.PageSize = products.Count();
            pointOfSaleViewModel.Customer = servicePool.CustomerService.GetCustomerByID(id);
            pointOfSaleViewModel.AvailableProducts = products.ToList();
            TempData["search"] = "Products Match The Name '  " + name + "  '";

            return View("Views/PointOfSale/SelectProductsFromSearch.cshtml", pointOfSaleViewModel);
        }

        //[HttpPost]
        public ActionResult StartServing(int id)
        {
            PointOfSaleViewModel pointOfSaleViewModel = new PointOfSaleViewModel();
            pointOfSaleViewModel.PageNumber = 1;
            pointOfSaleViewModel.PageSize = 12;
            pointOfSaleViewModel.Customer = servicePool.CustomerService.GetCustomerByID(id);
            pointOfSaleViewModel.AvailableProducts = servicePool.ProductService.GetAllProducts().ToList();
            return View("Views/PointOfSale/SelectProducts.cshtml", pointOfSaleViewModel);

        }




        [HttpGet]
        public ActionResult StartServingPagination(int id,int pageNumber = 1, int pageSize = 12)
        {
            PointOfSaleViewModel pointOfSaleViewModel = new PointOfSaleViewModel();
            if(pageSize == -1)
            {
                pointOfSaleViewModel.AvailableProducts = servicePool.ProductService.GetAllProducts().ToList();
            }
            else
            {
                pointOfSaleViewModel.AvailableProducts = servicePool.ProductService.GetProductByPage(pageNumber,pageSize).ToList();
            }
            pointOfSaleViewModel.Customer = servicePool.CustomerService.GetCustomerByID(id);
            pointOfSaleViewModel.PageNumber = pageNumber;
            pointOfSaleViewModel.PageSize = pageSize;
            return View("Views/PointOfSale/SelectProducts.cshtml", pointOfSaleViewModel);

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
            return View("Views/PointOfSale/SelectFromCustomers.cshtml", customers);
        }


        [HttpPost]
        public ActionResult GetCustomerCart(string name)
        {
            IEnumerable<CustomerDTO> customers =
                servicePool.CustomerService.GetAllCustomersByFilter(
                    customer =>
                    customer.Name.ToLower().Contains(name.ToLower())
                    );
            ViewData["search Query"] = name;
            return View("Views/PointOfSale/SelectFromCustomersThenGoToCart.cshtml", customers);
        }

        public IActionResult Carts()
        {
            return View("Views/PointOfSale/SelectCustomerForCart.cshtml");
        }

        [HttpPost]
        public IActionResult Cart(int id)
        {
            CustomerDTO customer = servicePool.CustomerService.GetCustomerByID(id);
            return View("Views/PointOfSale/Cart.cshtml", customer);
        }


        /// <summary>
        /// Json Handler
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("api/products")]
        public IActionResult ReceiveProducts([FromBody] List<JsonProduct> products)
        {
            List<ProductDTO> productsInformation = new List<ProductDTO>();

            foreach (JsonProduct product in products)
            {
                ProductDTO catchedProduct = servicePool.ProductService.GetProductByFilter
                    ((DBProduct) => DBProduct.Id == product.productId);
                catchedProduct.Quantity = product.quantity;
                productsInformation.Add(catchedProduct);
            }
            return Ok(Json(productsInformation));
        }


        [HttpPost]
        [Route("api/productsSubmit")]
        public IActionResult SubmitCart([FromBody] CartViewModel cart)
        {
            try
            {
                List<ProductDTO> productsToSave = new List<ProductDTO>();

                foreach (JsonProduct product in cart.products)
                {
                    ProductDTO catchedProduct = servicePool.ProductService.GetProductByFilter
                        ((DBProduct) => DBProduct.Id == product.productId);
                    if(catchedProduct.Quantity < product.quantity)
                    {
                        throw new Exception("Some One Takes The Product");
                    }
                    catchedProduct.Quantity = product.quantity;
                    productsToSave.Add(catchedProduct);
                }

                servicePool.CustomerService.AddOrder(cart.Id,productsToSave);

                return Ok("Done");
            }
            catch
            {
                return BadRequest("There Is Some Problems");
            }
        }
    }
}
