using System.ComponentModel.DataAnnotations;

namespace BillService.Dtos
{
    public class BillCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string BillDate { get; set; }

        [Required]
        public string BillAmount { get; set; }
    }
}