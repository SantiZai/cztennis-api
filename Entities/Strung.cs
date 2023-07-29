using System.ComponentModel.DataAnnotations;
namespace api.Entities
{
    public class Strung
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Image { get; set; }
        public float? Size { get; set; }
        public float? Price { get; set; }
    }
}
