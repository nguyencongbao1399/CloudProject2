using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingVoucher_v02.Database;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public class AppliedPromotionRepository : IAppliedPromotionRepository
    {
        private readonly MyDBContext _context;

        public AppliedPromotionRepository(MyDBContext context)
        {
            _context = context;
        }

        public bool Create(AppliedPromotion entity)
        {
            _context.AppliedPromotions.Add(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool Delete(AppliedPromotion entity)
        {
            _context.AppliedPromotions.Remove(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool ExistedId(int id)
        {
            return _context.AppliedPromotions.Any(e => e.Id == id);
        }

        public List<AppliedPromotion> GetAll()
        {
            return _context.AppliedPromotions.ToList();
        }

        public AppliedPromotion GetById(int id)
        {
            return _context.AppliedPromotions.Find(id);
        }

        public bool Update(AppliedPromotion entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            
            return _context.SaveChanges() >= 0;
        }

    }
}
