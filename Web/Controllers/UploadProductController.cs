using ApplicationLayer.DTOs;
using ApplicationLayer.ServicesProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Web.Models;
using Web.WebServices;

namespace Web.Controllers
{
    [Authorize]
    public class UploadProductController : BaseController
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IServerAssetsDisposal _serverAssetsDisposal;
        private readonly Web.AutoMapper.AutoMapper<UploadProductViewModel, ProductDTO> autoMapper;

        public UploadProductController(IServicePool servicePool,
            IWebHostEnvironment environment,IServerAssetsDisposal serverAssetsDisposal) : base(servicePool)
        {
            this._environment = environment;
            _serverAssetsDisposal = serverAssetsDisposal;
            autoMapper = new Web.AutoMapper.AutoMapper<UploadProductViewModel, ProductDTO>();
        }

        [HttpPost]
        public async Task<IActionResult> UploadProduct(UploadProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                IFormFile file = product.ImageFile;
                if (file == null || file.Length == 0 || !product.ImageFile.ContentType.StartsWith("image/"))
                {
                    TempData["Message"] = "Sorry the The Add Process Failed.";

                    TempData["Success"] = "False";
                    
                    return RedirectToAction("index", "Product");
                }
                else
                {

                    var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

                    fileName = "ProductsImages\\" + fileName;
                    var wwwrootPath = _environment.WebRootPath;


                    var filePath = Path.Combine(wwwrootPath, fileName);


                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    var imageUrl = $"https://localhost:44355/{fileName}";

                    product.Image = imageUrl;

                    ProductDTO productDTO = autoMapper.Map(product);

                    servicePool.ProductService.AddProduct(productDTO);

                    TempData["Message"] = "The Information Is Added Successfully.";

                    TempData["Success"] = "True";
                }
            }
            else
            {
                TempData["Message"] = "Sorry the The Add Process Failed.";

                TempData["Success"] = "False";
            }

            return RedirectToAction("index", "Product");

        }

        


        [HttpPost]
        public async Task<IActionResult> EditProduct(UploadProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                IFormFile file = product.ImageFile;
                if (file == null || file.Length == 0 || !product.ImageFile.ContentType.StartsWith("image/"))
                {
                    TempData["Success"] = "False";
                    TempData["Message"] = "Sorry the The Update Process Failed.";
                    return RedirectToAction("index", "Product");
                }
                else
                {
                    
                    String filePath = servicePool.ProductService.GetProductByFilter(e => e.Id == product.Id).Image;
                    var Namepart = filePath.Split('/')[3];

                    var result = _serverAssetsDisposal.DeleteImage(Namepart);


                    var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

                    fileName = "ProductsImages\\" + fileName;
                    var wwwrootPath = _environment.WebRootPath;


                    filePath = Path.Combine(wwwrootPath, fileName);


                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    var imageUrl = $"https://localhost:44355/{fileName}";
                    product.Image = imageUrl;
                    ProductDTO productDTO = autoMapper.Map(product);


                    servicePool.ProductService.UpdateProduct(productDTO.Id, productDTO);
                    TempData["Success"] = "True";
                    TempData["Message"] = "The Information Is Updated Successfully.";
                }
            }
            else
            {
                TempData["Success"] = "False";
                TempData["Message"] = "Sorry the The Update Process Failed.";
            }
            return RedirectToAction("index", "Product");

        }
    }
}
