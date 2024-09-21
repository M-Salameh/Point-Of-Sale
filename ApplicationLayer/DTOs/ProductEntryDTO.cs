using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.DTOs
{
	public class ProductEntryDTO
	{
		public ProductEntryDTO()
		{
			EntryItems = new HashSet<EntryItemsDTO>();
		}

		public int Id { get; set; }
		public int? SupplierId { get; set; }
		public DateTime? Date { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public virtual SupplierDTO Supplier { get; set; }
		public virtual ICollection<EntryItemsDTO> EntryItems { get; set; }
	}
}
