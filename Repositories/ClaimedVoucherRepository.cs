using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingVoucher_v02.Database;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public class ClaimedVoucherRepository : IClaimedVoucherRepository
    {
        private readonly MyDBContext _context;

        public ClaimedVoucherRepository(MyDBContext context)
        {
            _context = context;
        }

        public bool Create(ClaimedVoucher entity)
        {
            _context.ClaimedVouchers.Add(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool Delete(ClaimedVoucher entity)
        {
            _context.ClaimedVouchers.Remove(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool ExistedId(int id)
        {
            return _context.ClaimedVouchers.Any(e => e.Id == id);
        }

        public List<ClaimedVoucher> GetAll()
        {
            return _context.ClaimedVouchers.ToList();
        }

        public ClaimedVoucher GetById(int id)
        {
            return _context.ClaimedVouchers.Find(id);
        }

        public bool Update(ClaimedVoucher entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            
            return _context.SaveChanges() >= 0;
        }

    }
}
