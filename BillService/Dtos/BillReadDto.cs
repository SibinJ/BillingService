namespace BillService.Dtos
{
    public class BillReadDto
    {        
        public int Id { get; set; }

        public string Name { get; set; }
   
        public string BillDate { get; set; }
        
        public string BillAmount { get; set; }
    }
}