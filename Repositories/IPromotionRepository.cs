using System.Collections.Generic;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public interface IPromotionRepository
    {
        List<Promotion> GetAll();

        Promotion GetById(int id);

        bool Create(Promotion entity);

        bool Update(Promotion entity);

        bool Delete(Promotion entity);

        bool ExistedId(int id);
    }
}