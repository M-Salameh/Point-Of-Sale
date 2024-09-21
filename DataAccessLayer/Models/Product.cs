using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItems>();
            EntryItems = new HashSet<EntryItems>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public string Image { get; set; }
        public int? Quantity { get; set; }
        public int? AlertQuantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public virtual ICollection<EntryItems> EntryItems { get; set; }
    }
}
