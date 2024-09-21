using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class ProductEntry
    {
        public ProductEntry()
        {
            EntryItems = new HashSet<EntryItems>();
        }

        public int Id { get; set; }
        public int? SupplierId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<EntryItems> EntryItems { get; set; }
    }
}
