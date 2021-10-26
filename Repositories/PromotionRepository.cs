using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingVoucher_v02.Database;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly MyDBContext _context;

        public PromotionRepository(MyDBContext context)
        {
            _context = context;
        }

        public bool Create(Promotion entity)
        {
            _context.Promotions.Add(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool Delete(Promotion entity)
        {
            _context.Promotions.Remove(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool ExistedId(int id)
        {
            return _context.Promotions.Any(e => e.Id == id);
        }

        public List<Promotion> GetAll()
        {
            return _context.Promotions.ToList();
        }

        public Promotion GetById(int id)
        {
            return _context.Promotions.Find(id);
        }

        public bool Update(Promotion entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            
            return _context.SaveChanges() >= 0;
        }

    }
}
