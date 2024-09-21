using ApplicationLayer.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PointOfSaleViewModel
    {
        public PointOfSaleViewModel(List<ProductDTO> availableProducts, int pageNumber, int pageSize, int totalCount, CustomerDTO customer)
        {
            AvailableProducts = availableProducts;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Customer = customer;
        }

        public PointOfSaleViewModel(List<ProductDTO> products, CustomerDTO customer)
        {
            AvailableProducts = products;
            Customer = customer;
        }


        public PointOfSaleViewModel()
        {
            AvailableProducts = new List<ProductDTO>();
            Customer = new CustomerDTO();
        }


        [Display(Name = "AvailableProducts")]
        public List<ProductDTO> AvailableProducts { get; set; }


        [Display(Name = "PageNumber")]
        public int PageNumber { get; set; }



        [Display(Name = "PageSize")]
        public int PageSize { get; set; }



        [Display(Name = "TotalCount")]
        public int TotalCount { get; set; }





        [Display(Name = "Customer")]
        public CustomerDTO Customer { get; set; }
    }
}
