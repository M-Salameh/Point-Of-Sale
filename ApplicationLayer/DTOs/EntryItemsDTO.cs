using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationLayer.DTOs
{
    public class EntryItemsDTO
    {
        public int Id { get; set; }

        [Display(Name = "ProductEntryId")]
        public int? ProductEntryId { get; set; }
        public int? ProductId { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "Price")]
        public int? Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ProductDTO Product { get; set; }
        public virtual ProductEntryDTO ProductEntry { get; set; }
    }
}
