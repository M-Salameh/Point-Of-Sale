using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccessLayer.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        
    }
}
