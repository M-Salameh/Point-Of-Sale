using ApplicationLayer.DTOs;
using ApplicationLayer.Services.ApplicationServices.ProductServices;
using ApplicationLayer.ServicesProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Models;
using Web.WebServices;

namespace Web.Controllers
{

    [Authorize]
    public class ProductController : BaseController
    {
        IServerAssetsDisposal _serverAssetsDisposal;
        private readonly Web.AutoMapper.AutoMapper<UploadProductViewModel, ProductDTO> autoMapper;

        public ProductController(IServicePool servicePool, IServerAssetsDisposal serverAssetsDisposal) : base(servicePool)
        {
            this._serverAssetsDisposal = serverAssetsDisposal;
            autoMapper = new Web.AutoMapper.AutoMapper<UploadProductViewModel, ProductDTO>();
        }

        public IActionResult Index()
        {
            IEnumerable<ProductDTO> products = servicePool.ProductService.GetAllProducts();
            return View(products);
        }

        public IActionResult Add()
        {
            return View("Views/Product/Create.cshtml");
        }

        public IActionResult SelectFromProducts()
        {
            IEnumerable<ProductDTO> products = servicePool.ProductService.GetAllProducts();
            return View("Views/Product/SelectProduct.cshtml", products.ToList<ProductDTO>());
        }

        public IActionResult SelectThisProduct(int id)
        {
            return View("Views/Product/SelectProduct.cshtml");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                ProductDTO productToDelete = new ProductDTO();
                productToDelete.Id = id;
                String filePath = servicePool.ProductService.GetProductByFilter(e => e.Id == id).Image;
                var Namepart = filePath.Split('/')[3];

                servicePool.ProductService.DeleteProduct(productToDelete);
                var result = _serverAssetsDisposal.DeleteImage(Namepart);
                TempData["deleted"] = "True";
                TempData["Message"] = "The Product Deleted Successfully.";
            }
            catch (Exception)
            {
                TempData["deleted"] = "False";
                TempData["Message"] = "Sorry the The Delete Process Failed.";
            }
            //Thread.Sleep(2000);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public ActionResult Edit(int id)
        {
            ProductDTO productToEdit = new ProductDTO();
            UploadProductViewModel productViewModel = new UploadProductViewModel();

            try
            {
                productToEdit =
                    servicePool.ProductService.GetProductByID(id);
                productViewModel = autoMapper.Map(productToEdit);
            }
            catch (Exception exception)
            {
                TempData["exception"] = exception.Message;
            }

            return View("Edit", productViewModel);
        }



        [HttpGet]
        public ActionResult GetByName(string name)
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
            TempData["search"] = "Products Match The Name '  " + name + "  '";

            return View("Views/Product/Index.cshtml", products);
        }
    }
}
