using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.DTOs
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            OrderItems = new HashSet<OrderItemsDTO>();
        }

        public int Id { get; set; }
        public int? Discount { get; set; }
        public int? Tax { get; set; }
        public string Status { get; set; }
        public int? DeliveryPrice { get; set; }
        public string DeliveryAddress { get; set; }
        public int? CustomerId { get; set; }
        public int? TotalPrice { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual CustomerDTO CustomerDTO { get; set; }
        public virtual ICollection<OrderItemsDTO> OrderItems { get; set; }
    }
}
