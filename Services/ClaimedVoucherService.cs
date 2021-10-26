using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.Repositories;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.RequestModels.Update;
using TrackingVoucher_v02.Utils;

namespace TrackingVoucher_v02.Services
{
    public class ClaimedVoucherService : IClaimedVoucherService
    {
        private readonly IClaimedVoucherRepository _claimedRepo;
        private readonly IVoucherRepository _vouRepo;
        private readonly ValidateUtils util = new ValidateUtils();

        public ClaimedVoucherService(IClaimedVoucherRepository claimedRepo, IVoucherRepository vouRepo)
        {
            _claimedRepo = claimedRepo;
            _vouRepo = vouRepo;
        }
        public bool Create(ClaimedVoucherCreateRequest entity)
        {    
            int userId = entity.UserId;
            int voucherId = entity.VoucherId;
            int available = entity.Available;
            Voucher voucher = _vouRepo.GetAll().FirstOrDefault(e => e.Id == voucherId);
            if (available <= 0 || available > voucher.Available)
            {
                return false;
            }
            ClaimedVoucher existed = _claimedRepo.GetAll().FirstOrDefault(e => e.UserId == userId && e.VoucherId == voucherId);
            if (existed != null)
            {
                existed.Available += available;
                return _claimedRepo.Update(existed);
            }

            ClaimedVoucher newEntity = new ClaimedVoucher();
            newEntity.Available = available;
            newEntity.ClaimedDate = DateTime.Now;
            newEntity.ExpiredDate = DateTime.Now.AddDays(30);
            newEntity.UserId = userId;
            newEntity.VoucherId = voucherId;
            bool success = _claimedRepo.Create(newEntity);

            //update voucher available
            if (success)
            {
                voucher.Available -= available;
                success = _vouRepo.Update(voucher);
            }

            return success;
        }

        public bool Delete(int id)
        {
            var entity = _claimedRepo.GetById(id);
            if (entity == null)
            {
                return false;
            }
            return _claimedRepo.Delete(entity);
        }

        public bool DeleteByUserId(int userId)
        {
            bool success = true;
            IEnumerable<ClaimedVoucher> list = _claimedRepo.GetAll().Where(p => p.UserId == userId);
            foreach (ClaimedVoucher item in list)
            {
                success = _claimedRepo.Delete(item);
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteByVoucherId(int voucherId)
        {
            bool success = true;
            IEnumerable<ClaimedVoucher> list = _claimedRepo.GetAll().Where(p => p.VoucherId == voucherId);
            foreach (ClaimedVoucher item in list)
            {
                success = _claimedRepo.Delete(item);
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<ClaimedVoucher> GetAll(string query, int userId, int voucherId, string ids,
            int startId, DateTime claimedDate, DateTime expiredDate)
        {
            //initialize a list
            IEnumerable<ClaimedVoucher> list = _claimedRepo.GetAll();

            //case has query
            if (query.Length > 0)
            {
                //validate

                if (query.Contains("user-id="))
                {
                    if (!util.ValidIntParam(query, "user-id=", userId))
                    {
                        return null;
                    }
                }

                if (query.Contains("voucher-id="))
                {
                    if (!util.ValidIntParam(query, "voucher-id=", voucherId))
                    {
                        return null;
                    }
                }
       
                if (query.Contains("start-id="))
                {
                    if (!util.ValidIntParam(query, "id=", startId))
                    {
                        return null;
                    }
                }
        
                if (query.Contains("ids="))
                {
                    if (!util.ValidIntParams(query, "ids=", ids))
                    {
                        return null;
                    }
                }
         
                if (query.Contains("claimed-date="))
                {
                    if (!util.ValidDateTimeParam(query, "claimed-date=", claimedDate))
                    {
                        return null;
                    }
                }
        
                if (query.Contains("expired-date="))
                {
                    if (!util.ValidDateTimeParam(query, "expired-date=", expiredDate))
                    {
                        return null;
                    }
                }

                //id, ids must not in a uri
                if (query.Contains("user-id=") && query.Contains("ids="))
                {
                    return null;
                }

                //page only involve with limit and fields
                //not filter changing list item
                if (query.Contains("page=") && (query.Contains("user-id=")
                                           || query.Contains("ids=")
                                           || query.Contains("start-id=")))
                {
                    return null;
                }

                //query must have both begin-date and expired-date
                if(query.Contains("claimed-date=") && !query.Contains("expired-date=")
                    || !query.Contains("claimed-date=") && query.Contains("expired-date="))
                {
                    return null;
                }

                //begin-date must less than expired-date
                if (claimedDate.CompareTo(expiredDate) == 1)
                {
                    return null;
                }

                //case param
                if (!query.Contains("&"))
                {

                    //user-id
                    if (query.Contains("user-id="))
                    {
                        return list.Where(e => e.UserId == userId);
                    }

                    //voucher-id
                    if (query.Contains("voucher-id="))
                    {
                        return list.Where(e => e.VoucherId == voucherId);
                    }

                    //ids
                    if (query.Contains("ids="))
                    {
                        return list.Where(e => ids.Contains(e.Id.ToString()));
                    }

                    //startId
                    if (query.Contains("start-id="))
                    {
                        return list.Where(e => e.Id >= startId);
                    }
                }

                //case params
                if (query.Contains("&"))
                {
                    //case params: name, address
                    if (query.Contains("claimed-date=") & query.Contains("expired-date="))
                    {
                        return list.Where(e => e.ClaimedDate.CompareTo(claimedDate) == 1
                        && e.ExpiredDate.CompareTo(expiredDate) == -1);
                    }
                }
                return null;
            } // end if uri has query

            //case uri has no query
            return list;
        }

        public ClaimedVoucher GetById(int id)
        {
            return _claimedRepo.GetById(id);
        }

        public int GetCount()
        {
            return _claimedRepo.GetAll().Count();
        }

        public bool Update(ClaimedVoucherUpdateRequest entity)
        {
            int available = entity.Available;
            ClaimedVoucher existed = _claimedRepo.GetById(entity.Id);
            if (existed == null)
            {
                return false;
            }
            if (available == existed.Available)
            {
                return false;
            }
            existed.Available = available;
            existed.LastUsedDate = DateTime.Now;

            return _claimedRepo.Update(existed);
        }
    }
}
