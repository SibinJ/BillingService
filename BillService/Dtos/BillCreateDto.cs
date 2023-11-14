using System.ComponentModel.DataAnnotations;

namespace BillService.Dtos
{
    public class BillCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Publisher { get; set; }

        [Required]
        public string Cost { get; set; }
    }
}