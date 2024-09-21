using System;

namespace ApplicationLayer.DTOs
{
    public class OrderItemsDTO
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual OrderDTO Order { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
