using System.ComponentModel.DataAnnotations;

namespace api.Entities
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public bool? IsAdmin { get; set; } = false;
        public List<Order>? Orders { get; set; } = new List<Order>();
    }
}
