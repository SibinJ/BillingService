using Microsoft.EntityFrameworkCore;
using BillService.Models;

namespace BillService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Bill> Bills { get; set; }
    }
}