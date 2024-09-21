using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccessLayer.Models
{
	public partial class Customer
	{
		public Customer()
		{
			Order = new HashSet<Order>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		
		[AllowNull]
		public string Address { get; set; }
		
		[AllowNull]
		public string EmailAddress { get; set; }

		[AllowNull]
		public string PhoneNumber { get; set; }

		[AllowNull]
		public DateTime CreatedAt { get; set; }
		
		[AllowNull]
		public DateTime UpdatedAt { get; set; }

		public virtual ICollection<Order> Order { get; set; }

	}
}
