using System.Collections.Generic;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public interface IBrandRepository
    {
        List<Brand> GetAll();

        Brand GetById(int id);

        bool Create(Brand entity);

        bool Update(Brand entity);

        bool Delete(Brand entity);

        bool ExistedId(int id);
    }
}