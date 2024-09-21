using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccessLayer.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItems>();
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
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
