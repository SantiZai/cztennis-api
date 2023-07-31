using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    public class Order
    {
        public enum STATUS
        {
            Pendiente,
            Pagado,
            Cancelado,
        }

        [Key]
        [Required]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int? User_Id { get; set; }
        [ForeignKey("Strung")]
        public int? Strung_Id { get; set; }
        public STATUS? Status { get; set; } = STATUS.Pendiente;
    }
}
