using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccessLayer.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            ProductEntry = new HashSet<ProductEntry>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<ProductEntry> ProductEntry { get; set; }
    }
}
