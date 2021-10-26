using System.Collections.Generic;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public interface IClaimedVoucherRepository
    {
        List<ClaimedVoucher> GetAll();

        ClaimedVoucher GetById(int id);

        bool Create(ClaimedVoucher entity);

        bool Update(ClaimedVoucher entity);

        bool Delete(ClaimedVoucher entity);

        bool ExistedId(int id);
    }
}