using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingVoucher_v02.Database;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly MyDBContext _context;

        public VoucherRepository(MyDBContext context)
        {
            _context = context;
        }

        public bool Create(Voucher entity)
        {
            _context.Vouchers.Add(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool Delete(Voucher entity)
        {
            _context.Vouchers.Remove(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool ExistedId(int id)
        {
            return _context.Vouchers.Any(e => e.Id == id);
        }

        public List<Voucher> GetAll()
        {
            return _context.Vouchers.ToList();
        }

        public Voucher GetById(int id)
        {
            return _context.Vouchers.Find(id);
        }

        public bool Update(Voucher entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            
            return _context.SaveChanges() >= 0;
        }

    }
}
