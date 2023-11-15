using System.Collections.Generic;
using BillService.Models;

namespace BillService.Data
{
    public interface IBillRepo
    {
        bool SaveChanges();

        IEnumerable<Bill> GetAllBills();
        Bill GetBillById(int id);
        void CreateBill(Bill bill);
    }
}