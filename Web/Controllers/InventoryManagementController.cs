using ApplicationLayer.DTOs;
using ApplicationLayer.ServicesProvider;
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
	/// Handel the Inventory Actions.
	/// </summary>
	
    [Authorize]
    public class InventoryManagementController : BaseController
	{

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="servicePool"></param>
		public InventoryManagementController(IServicePool servicePool) : base(servicePool)
		{
		}

		/// <summary>
		/// Actions Starter.
		/// </summary>
		/// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// Select The Supplier To See The Transaction That Came From Him.
		/// </summary>
		/// <returns></returns>
        public IActionResult SelectSupplier()
        {
            return View("Views/InventoryManagement/SelectSupplier.cshtml");
        }

        /// <summary>
        /// Get The Suppliers By There Name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSupplierByName(string name)
        {
            IEnumerable<SupplierDTO> suppliers =
                servicePool.SupplierService.GetAllSuppliersByFilter(
                    supplier =>
                    supplier.Name.ToLower().Contains(name.ToLower()));
            ViewData["search Query"] = name;
            return View("Views/InventoryManagement/SelectFromSuppliers.cshtml", suppliers);
        }





        /// <summary>
        /// Get The Suppliers By There Name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSupplierByNameTwo(string name)
        {
            IEnumerable<SupplierDTO> suppliers =
                servicePool.SupplierService.GetAllSuppliersByFilter(
                    supplier =>
                    supplier.Name.ToLower().Contains(name.ToLower()));
            ViewData["search Query"] = name;
            return View("Views/InventoryManagement/SelectFromSuppliersTwo.cshtml", suppliers);
        }


        /// <summary>
        /// Add new Supplier
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
		{
			return View("Views/InventoryManagement/CreateSupplier.cshtml");
		}


		/// <summary>
		/// Post Action To Create A New Supplier To Get Items From.
		/// </summary>
		/// <param name="supplier"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Create(SupplierDTO supplier)
		{
			if (ModelState.IsValid)
			{

				var Id = servicePool.SupplierService.AddSupplierAndGetId(supplier);

                return RedirectToAction(nameof(StartBuying), new RouteValueDictionary(new
                {
                    Controller = "InventoryManagement",
                    Action = "StartBuying",
                    id = Id
                }));
            }
			else
			{
				TempData["Success"] = "False";
				return RedirectToAction(nameof(Index));
			}
		}


		/// <summary>
		/// Redirector.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GoToProducts(int id)
		{

            return RedirectToAction(nameof(StartBuying),new RouteValueDictionary(new 
			{
				Controller = "InventoryManagement",
				Action = "StartBuying",
				id = id
            }
			));
		}



        /// <summary>
        /// Redirector.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GoToEntries(int id)
        {

            return RedirectToAction(nameof(Entries), new RouteValueDictionary(new
            {
                Controller = "InventoryManagement",
                Action = "Entries",
                id = id
            }
            ));
        }



        #warning Not Working Now.......................
        /// <summary>
        /// The Action That Returns The Products that get by supplier with id = ${id}.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Entries(int id)
        {
            try
            {

            List<ProductEntryDTO> entries = 
                (List<ProductEntryDTO>)servicePool.ProductEntryService.GetAllProductEntrysByFilter(
                (entry)=> { 
                    return entry.SupplierId == id;
                }
                );

                string name = servicePool.SupplierService.GetSupplierByID(id).Name;
                ViewData["Supplier"] = name;

            return View("Views/InventoryManagement/Entries.cshtml", entries);
            } catch(Exception)
            {
                return View("Views/Errors/500.cshtml");

            }
        }



        /// <summary>
        /// The Action That Returns The Products.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult StartBuying(int id)
		{
			InventoryManagementViewModel inventoryManagementViewModel = new InventoryManagementViewModel();
			inventoryManagementViewModel.Supplier = servicePool.SupplierService.GetSupplierByID(id);
			inventoryManagementViewModel.Products = servicePool.ProductService.GetAllProducts().ToList();
			return View("Views/InventoryManagement/SelectProducts.cshtml", inventoryManagementViewModel);
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="supplierId"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult AddQuantity(int id, int supplierId)
		{
			SupplierEntry supplierEntry = new SupplierEntry();
			

			try
			{
				supplierEntry.Product = servicePool.ProductService.GetProductByID(id);
				supplierEntry.Supplier = servicePool.SupplierService.GetSupplierByID(supplierId);
			}
			catch (Exception exception)
			{
				TempData["exception"] = exception.Message;
			}

			return View("AddQuantity", supplierEntry);
		}

		[HttpGet]
		public ActionResult GetProductByName(string name = "",int supplierId = 0)
		{
            try
			{
				if (name == "" || supplierId == 0)
                {
                    InventoryManagementViewModel inventoryManagementViewModel = new InventoryManagementViewModel();
                    inventoryManagementViewModel.Supplier = servicePool.SupplierService.GetSupplierByID(supplierId);
                    inventoryManagementViewModel.Products = servicePool.ProductService.GetAllProductsByFilter(
                        product =>
                        product.Name.ToLower().Contains(name.ToLower())
                        ).ToList();
                    return View("Views/InventoryManagement/SelectProducts.cshtml", inventoryManagementViewModel);
                }
                else
                {
                    InventoryManagementViewModel inventoryManagementViewModel = new InventoryManagementViewModel();
                    inventoryManagementViewModel.Supplier = servicePool.SupplierService.GetSupplierByID(supplierId);
                    inventoryManagementViewModel.Products = servicePool.ProductService.GetAllProducts().ToList();
                    return View("Views/InventoryManagement/SelectProducts.cshtml", inventoryManagementViewModel);
                }
            }
			catch(Exception e)
			{
				return View("Views/InventoryManagement/Index.cshtml");
			}
		}

		
			

		[HttpPost]
        public ActionResult AddEntry(SupplierEntry supplierEntry)
        {
            try
            {
				servicePool.SupplierService.AddProducts(supplierEntry.Supplier.Id, supplierEntry.Product.Id,
					supplierEntry.Product.Quantity, supplierEntry.Product.Price); 
            }
            catch (Exception exception)
            {
                TempData["exception"] = exception.Message;
            }

			return RedirectToAction(nameof(StartBuying), new RouteValueDictionary(new
			{
				Controller = "InventoryManagement",
				Action = "StartBuying",
				id = supplierEntry.Supplier.Id
			}));
        }
    }
}
