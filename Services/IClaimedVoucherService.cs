using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.RequestModels.Update;

namespace TrackingVoucher_v02.Services
{
    public interface IClaimedVoucherService
    {
        public bool Create(ClaimedVoucherCreateRequest entity);

        public bool Update(ClaimedVoucherUpdateRequest entity);

        public bool Delete(int id);

        public bool DeleteByVoucherId(int voucherId);

        public bool DeleteByUserId(int userId);

        public IEnumerable<ClaimedVoucher> GetAll(string query, int userId, int voucherId, string ids,
            int startId, DateTime claimedDate, DateTime expiredDate);

        public ClaimedVoucher GetById(int id);
        public int GetCount();
    }
}
