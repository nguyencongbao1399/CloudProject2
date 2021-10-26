using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingVoucher_v02.Database;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDBContext _context;

        public UserRepository(MyDBContext context)
        {
            _context = context;
        }

        public bool Create(User entity)
        {
            _context.Users.Add(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool Delete(User entity)
        {
            _context.Users.Remove(entity);

            return _context.SaveChanges() >= 0;
        }

        public bool ExistedFacebook(string facebook)
        {
            return _context.Users.Any(e => e.Facebook.Equals(facebook));
        }

        public bool ExistedGmail(string gmail)
        {
            return _context.Users.Any(e => e.Gmail.Equals(gmail));
        }

        public bool ExistedId(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetByFacebook(string facebook)
        {
            return _context.Users.FirstOrDefault(e => e.Facebook.Equals(facebook));
        }

        public User GetByGmail(string gmail)
        {
            return _context.Users.FirstOrDefault(e => e.Gmail.Equals(gmail));
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public bool Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            
            return _context.SaveChanges() >= 0;
        }

    }
}
