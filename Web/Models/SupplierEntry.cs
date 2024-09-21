using ApplicationLayer.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class SupplierEntry
    {

        public SupplierEntry(SupplierDTO supplier, ProductDTO product)
        {
            this.Supplier = supplier;
            this.Product = product;
        }

        public SupplierEntry()
        {

            Product = new ProductDTO();
            Supplier = new SupplierDTO();
        }



        [Display(Name = "supplier")]
        public SupplierDTO Supplier { get; set; }

        [Display(Name = "product")]
        public ProductDTO Product { get; set; }

    }
}