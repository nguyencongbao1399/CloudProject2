using System.Collections.Generic;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;

namespace TrackingVoucher_v02.Services
{
    public interface IAppliedPromotionService
    {
        public bool Create(AppliedPromotionCreateRequest entity);

        public bool Delete(int id);

        public bool DeleteByPromotionId(int proId);

        public bool DeleteByStoreId(int storeId);

        public IEnumerable<AppliedPromotion> GetAll(string query, int storeId, int promotionId, string ids, int startId);

        public AppliedPromotion GetById(int id);
        public int GetCount();
    }
}
