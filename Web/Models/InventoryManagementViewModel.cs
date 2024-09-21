using ApplicationLayer.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class InventoryManagementViewModel
    {
        
        public InventoryManagementViewModel(SupplierDTO supplier, List<ProductDTO> products)
        {
            this.Supplier = supplier;
            this.Products = products;
        }
        
        public InventoryManagementViewModel() {

            Products = new List<ProductDTO>();
            Supplier = new SupplierDTO();
        }



        [Display(Name ="supplier")]
        public SupplierDTO Supplier { get; set; }

        [Display(Name = "products")]
        public List<ProductDTO> Products { get; set; }

    }
}