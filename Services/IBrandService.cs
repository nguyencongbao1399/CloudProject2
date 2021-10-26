using System.Collections.Generic;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.RequestModels.Update;

namespace TrackingVoucher_v02.Services
{
    public interface IBrandService
    {
        public bool Create(BrandCreateRequest entity);

        public bool Update(BrandUpdateRequest entity);

        public bool Delete(int id);

        public IEnumerable<Brand> GetAll(string query, string name, string address, string ids, int startId);

        public Brand GetById(int id);
        public int GetCount();
    }
}
