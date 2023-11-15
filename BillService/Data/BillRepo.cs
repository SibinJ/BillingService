using System;
using System.Collections.Generic;
using System.Linq;
using BillService.Models;

namespace BillService.Data
{
    public class BillRepo : IBillRepo
    {
        private readonly AppDbContext _context;

        public BillRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateBill(Bill bill)
        {
            if(bill == null)
            {
                throw new ArgumentNullException(nameof(bill));
            }

            _context.Bills.Add(bill);
        }

        public IEnumerable<Bill> GetAllBills()
        {
            return _context.Bills.ToList();
        }

        public Bill GetBillById(int id)
        {
            return _context.Bills.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}