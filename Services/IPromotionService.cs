using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.RequestModels.Update;

namespace TrackingVoucher_v02.Services
{
    public interface IPromotionService
    {
        public bool Create(PromotionCreateRequest entity);

        public bool Update(PromotionUpdateRequest entity);

        public bool Delete(int id);

        public bool DeleteByBrandId(int brandId);

        public IEnumerable<Promotion> GetAll(string query, int brandId, string ids,
            int startId, DateTime beginDate, DateTime expiredDate);

        public Promotion GetById(int id);
        public int GetCount();
    }
}
