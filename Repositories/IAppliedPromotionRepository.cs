using System.Collections.Generic;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public interface IAppliedPromotionRepository
    {
        List<AppliedPromotion> GetAll();

        AppliedPromotion GetById(int id);

        bool Create(AppliedPromotion entity);

        bool Update(AppliedPromotion entity);

        bool Delete(AppliedPromotion entity);

        bool ExistedId(int id);
    }
}