using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.RequestModels.Update;

namespace TrackingVoucher_v02.Services
{
    public interface IUserService
    {
        public bool Create(UserCreateRequest entity);

        public bool Update(UserUpdateRequest entity);

        public bool Delete(int id);

        public IEnumerable<User> GetAll(string query, string name, string address, string ids,
            int startId);

        public User GetById(int id);

        public User GetByGmail(string gmail);

        public User GetByFacebook(string facebook);

        public int GetCount();
    }
}
