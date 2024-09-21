using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DataAccessLayer.Models
{
	public class MarketContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
	{
		public MarketContext()
		{
		}

		public MarketContext(DbContextOptions<MarketContext> options)
			: base(options)
		{
		}

		public  DbSet<Order> Order { get; set; }
		public  DbSet<Product> Product { get; set; }
		public  DbSet<Customer> Customer { get; set; }
		public  DbSet<Supplier> Supplier { get; set; }
		public  DbSet<Employee> Employee { get; set; } 
		public  DbSet<OrderItems> OrderItems { get; set; }
		public  DbSet<EntryItems> EntryItems { get; set; }
		public  DbSet<ProductEntry> ProductEntry { get; set; }
		public  DbSet<Notification> Notification { get; set; }
		public  DbSet<ApplicationUser> ApplicationUser { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server =M-SALAMEH\\SQLEXPRESS; Database = MarketMiniProject; Trusted_Connection = True;");
			}
		}

		

	}
}
