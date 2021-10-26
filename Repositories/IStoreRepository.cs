using System.Collections.Generic;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public interface IStoreRepository
    {
        List<Store> GetAll();

        Store GetById(int id);

        bool Create(Store entity);

        bool Update(Store entity);

        bool Delete(Store entity);

        bool ExistedId(int id);
    }
}