using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingVoucher_v02.Database;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly MyDBContext _context;

        public StoreRepository(MyDBContext context)
        {
            _context = context;
        }

        public bool Create(Store entity)
        {
            _context.Stores.Add(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool Delete(Store entity)
        {
            _context.Stores.Remove(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool ExistedId(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }

        public List<Store> GetAll()
        {
            return _context.Stores.ToList();
        }

        public Store GetById(int id)
        {
            return _context.Stores.Find(id);
        }

        public bool Update(Store entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            
            return _context.SaveChanges() >= 0;
        }

    }
}
