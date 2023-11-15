using System.ComponentModel.DataAnnotations;

namespace BillService.Models
{
    public class Bill
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BillDate { get; set; }

        [Required]
        public string BillAmount { get; set; }
    }
}