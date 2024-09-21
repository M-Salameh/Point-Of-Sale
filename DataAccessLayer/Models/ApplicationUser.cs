using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
	[Table(nameof(ApplicationUser))]
	public class ApplicationUser : IdentityUser<string>
	{
		public virtual Employee Employee { get; set; }
	}
}
