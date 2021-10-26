using System.Collections.Generic;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public interface IVoucherRepository
    {
        List<Voucher> GetAll();

        Voucher GetById(int id);

        bool Create(Voucher entity);

        bool Update(Voucher entity);

        bool Delete(Voucher entity);

        bool ExistedId(int id);
    }
}