using ApplicationLayer.DTOs;
using ApplicationLayer.ServicesProvider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Web.Controllers
{
    [Authorize]
    public class ProductEntryController : BaseController
    {
        public ProductEntryController(IServicePool servicePool) : base(servicePool)
        {
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            List<EntryItemsDTO> itemsDTOs =
                servicePool.EntryItemsService.GetAllEntryItemssByFilter((entry) =>
                {
                    return entry.ProductEntryId == id;
                }).ToList();
            foreach(var item in itemsDTOs)
            {
                item.Product = servicePool.ProductService.GetProductByID(item.ProductId);
            }
            return View("Views/ProductEntry/index.cshtml",itemsDTOs);
        }
    }
}
