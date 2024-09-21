using Microsoft.AspNetCore.Mvc;
using ApplicationLayer.ServicesProvider;
using ApplicationLayer.DTOs;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
	[Authorize]
	public class SupplierController : BaseController
	{
		public SupplierController(IServicePool servicePool)
			: base(servicePool)
		{

		}


		[HttpGet]
        public ActionResult GetByName(string name)
        {
			if (string.IsNullOrEmpty(name))
			{
				return View("Views/Supplier/Index.cshtml", servicePool.SupplierService.GetAllSuppliers());
            }
			IEnumerable<SupplierDTO> suppliers =
				servicePool.SupplierService.GetAllSuppliersByFilter(
					supplier => 
					supplier.Name.ToLower().Contains(name.ToLower())
					);
            @TempData["search"] = "Suppliers Match The Name '  " + name + "  '";

            return View("Views/Supplier/Index.cshtml", suppliers);
        }

        public ActionResult Index()
		{

			IEnumerable<SupplierDTO> suppliers = servicePool.SupplierService.GetAllSuppliers();
			return View(suppliers);
		}
		[HttpPost]
		public ActionResult Delete(int id)
		{
			try
			{
				SupplierDTO supplier = new SupplierDTO();
				supplier.Id = id;

				servicePool.SupplierService.DeleteSupplier(supplier);
				TempData["deleted"] = "True";
                TempData["Message"] = "The Supplier Deleted Successfully.";

            }
            catch (Exception)
			{
				TempData["deleted"] = "False";
                TempData["Message"] = "Sorry the The Delete Process Failed.";
            }
            return RedirectToAction(nameof(Index));
		}

		public ActionResult Add()
		{
			return View("Views/Supplier/Create.cshtml");
		}

		
		[HttpPost]
		public ActionResult Create(SupplierDTO supplierDTO)
		{
			if (ModelState.IsValid)
			{
				servicePool.SupplierService.AddSupplier(supplierDTO);
                TempData["Message"] = "The Information Is Added Successfully.";

                TempData["Success"] = "True";

			}
			else
			{
                TempData["Message"] = "Sorry the The Add Process Failed.";

                TempData["Success"] = "False";
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
			SupplierDTO supplierDTO = new SupplierDTO();

            try
            {
                supplierDTO = servicePool.SupplierService.GetSupplierByID(id);
            }
            catch (Exception)
			{
				return View("Views/Errors/500.cshtml");
            }

            return View("Edit", supplierDTO);
        }




		/// <summary>
		/// update a Supplier
		/// </summary>
		/// <param name="supplier"></param>
		/// <returns></returns>
		
        [HttpPost]
        public ActionResult Update(SupplierDTO supplier)
        {
            if (ModelState.IsValid)
            {
                servicePool.SupplierService.UpdateSupplier(supplier.Id, supplier);
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
    }
}
