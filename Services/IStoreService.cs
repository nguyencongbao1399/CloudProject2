using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.RequestModels.Update;

namespace TrackingVoucher_v02.Services
{
    public interface IStoreService
    {
        public bool Create(StoreCreateRequest entity);

        public bool Update(StoreUpdateRequest entity);

        public bool Delete(int id);

        public bool DeleteByBrandId(int brandId);

        public IEnumerable<Store> GetAll(string query, int brandId, string name, string address,
            string ids, int startId);

        public Store GetById(int id);
        public int GetCount();
    }
}
