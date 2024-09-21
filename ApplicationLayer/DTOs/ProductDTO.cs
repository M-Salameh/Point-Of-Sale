using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ApplicationLayer.DTOs
{
	public class ProductDTO
	{
		public ProductDTO()
		{
			OrderItems = new HashSet<OrderItemsDTO>();
			EntryItems = new HashSet<EntryItemsDTO>();
		}

		public int Id { get; set; }


		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

        [Required(ErrorMessage = "Enter The Price Of The Product Please..🙂")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be non-negative.")]
        public int Price { get; set; }
		[AllowNull]
		public string Image { get; set; }



		[DefaultValue(0)]
        [Required(ErrorMessage = "Enter The Price Of The Product Please..🙂")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be non-negative.")]
        public int Quantity { get; set; }


        [DefaultValue(0)]
        [Required(ErrorMessage = "Enter The Price Of The Product Please..🙂")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be non-negative.")]
        public int AlertQuantity { get; set; }


        public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public virtual ICollection<OrderItemsDTO> OrderItems { get; set; }
		public virtual ICollection<EntryItemsDTO> EntryItems { get; set; }
	}
}
