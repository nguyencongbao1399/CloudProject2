using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingVoucher_v02.Database;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly MyDBContext _context;

        public BrandRepository(MyDBContext context)
        {
            _context = context;
        }

        public bool Create(Brand entity)
        {
            _context.Brands.Add(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool Delete(Brand entity)
        {
            _context.Brands.Remove(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool ExistedId(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }

        public List<Brand> GetAll()
        {
            return _context.Brands.ToList();
        }

        public Brand GetById(int id)
        {
            return _context.Brands.Find(id);
        }

        public bool Update(Brand entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            
            return _context.SaveChanges() >= 0;
        }

    }
}
