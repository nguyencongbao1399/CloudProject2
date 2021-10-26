using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.RequestModels.Update;

namespace TrackingVoucher_v02.Services
{
    public interface IVoucherService
    {
        public bool Create(VoucherCreateRequest entity);

        public bool Update(VoucherUpdateRequest entity);

        public bool Delete(int id);

        public bool DeleteByPromotionId(int proId);

        public IEnumerable<Voucher> GetAll(string query, int brandId, string ids, int startId,
            DateTime beginDate, DateTime expiredDate);

        public Voucher GetById(int id);
        public int GetCount();
    }
}
