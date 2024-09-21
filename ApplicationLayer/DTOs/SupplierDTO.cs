using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.DTOs
{
	public class SupplierDTO
	{
		public SupplierDTO()
		{
			ProductEntry = new HashSet<ProductEntryDTO>();
		}
        public SupplierDTO(int id,string name)
        {
            ProductEntry = new HashSet<ProductEntryDTO>();
			this.Id = id;
			this.Name = name;
        }
        public int Id { get; set; }


		[Required (ErrorMessage = "Enter The Name Of The Supplier Please..🙂")]
        public string Name { get; set; }
		

		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public virtual ICollection<ProductEntryDTO> ProductEntry { get; set; }
	}
}
