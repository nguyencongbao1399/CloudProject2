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
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _proRepo;
        private readonly IAppliedPromotionService _appliedSer;
        private readonly IVoucherService _vouSer;
        private readonly ValidateUtils util = new ValidateUtils();

        public PromotionService(IPromotionRepository proRepo, IAppliedPromotionService appliedSer, IVoucherService vouSer)
        {
            _proRepo = proRepo;
            _appliedSer = appliedSer;
            _vouSer = vouSer;
        }
        public bool Create(PromotionCreateRequest entity)
        {
            string description = entity.Description;
            DateTime beginDate = entity.BeginDate;
            DateTime expiredDate = entity.ExpiredDate;

            if (!util.ValidRangeLengthInput(description, 1, 250)
                || beginDate == null
                || expiredDate == null
                || beginDate.CompareTo(expiredDate) >= 0)
            {
                return false;
            }

            Promotion existed = _proRepo.GetAll()
                .FirstOrDefault(e => e.Description.Trim().ToLower().Equals(description.Trim().ToLower()));
            if (existed != null)
            {
                return false;
            }
            Promotion newEntity = new Promotion();
            newEntity.Description = description.Trim();
            newEntity.BeginDate = beginDate;
            newEntity.BrandId = entity.BrandId;
            
            return _proRepo.Create(newEntity);
        }

        public bool Delete(int id)
        {
            _appliedSer.DeleteByPromotionId(id);
            var entity = _proRepo.GetById(id);
            if (entity == null)
            {
                return false;
            }
            return _proRepo.Delete(entity);
        }

        public bool DeleteByBrandId(int brandId)
        {
            bool success = true;
            IEnumerable<Promotion> list = _proRepo.GetAll().Where(p => p.BrandId == brandId);
            foreach (Promotion item in list)
            {
                _appliedSer.DeleteByPromotionId(item.Id);
                _vouSer.DeleteByPromotionId(item.Id);
                success = _proRepo.Delete(item);
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<Promotion> GetAll(string query, int brandId, string ids, 
            int startId, DateTime beginDate, DateTime expiredDate)
        {
            //initialize a list
            IEnumerable<Promotion> list = _proRepo.GetAll();

            //case has query
            if (query.Length > 0)
            {
                //validate

                if (query.Contains("brand-id="))
                {
                    if (!util.ValidIntParam(query, "brand-id=", brandId))
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
         
                if (query.Contains("begin-date="))
                {
                    if (!util.ValidDateTimeParam(query, "begin-date=", beginDate))
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
                if (query.Contains("brand-id=") && query.Contains("ids="))
                {
                    return null;
                }

                //page only involve with limit and fields
                //not filter changing list item
                if (query.Contains("page=") && (query.Contains("brand-id=")
                                           || query.Contains("ids=")
                                           || query.Contains("start-id=")))
                {
                    return null;
                }

                //query must have both begin-date and expired-date
                if(query.Contains("begin-date=") && !query.Contains("expired-date=")
                    || !query.Contains("begin-date=") && query.Contains("expired-date="))
                {
                    return null;
                }

                //begin-date must less than expired-date
                if (beginDate.CompareTo(expiredDate) == 1)
                {
                    return null;
                }

                //case param
                if (!query.Contains("&"))
                {

                    //id
                    if (query.Contains("brand-id="))
                    {
                        return list.Where(e => e.BrandId == brandId);
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

                    //limit must be last
                    if (query.Contains("limit="))
                    {
                        return list;
                    }
                }

                //case params
                if (query.Contains("&"))
                {
                    //case params: name, address
                    if (query.Contains("begin-date=") & query.Contains("expired-date="))
                    {
                        return list.Where(e => e.BeginDate.CompareTo(beginDate) == 1
                        && e.ExpiredDate.CompareTo(expiredDate) == -1);
                            
                    }

                }
                return null;
            } // end if uri has query

            //case uri has no query
            return list;
        }

        public Promotion GetById(int id)
        {
            return _proRepo.GetById(id);
        }

        public int GetCount()
        {
            return _proRepo.GetAll().Count();
        }

        public bool Update(PromotionUpdateRequest entity)
        {
            string description = entity.Description;
            DateTime beginDate = entity.BeginDate;
            DateTime expiredDate = entity.ExpiredDate;

            Promotion existed = _proRepo.GetById(entity.Id);
            if (existed == null)
            {
                return false;
            }

            if(description != null)
            {
                if (!util.ValidRangeLengthInput(description, 1, 250))
                {
                    return false;
                }
                existed.Description = description;
            }
            if (beginDate != null && expiredDate != null)
            {
                if(beginDate.CompareTo(expiredDate) >= 0)
                {
                    return false;
                }
                existed.BeginDate = beginDate;
                existed.ExpiredDate = expiredDate;
            }
            if (beginDate != null && expiredDate == null)
            {
                if (beginDate.CompareTo(existed.ExpiredDate) >= 0)
                {
                    return false;
                }
                existed.BeginDate = beginDate;
            }
            if (beginDate == null && expiredDate != null)
            {
                if (existed.BeginDate.CompareTo(expiredDate) >= 0)
                {
                    return false;
                }
                existed.ExpiredDate = expiredDate;
            }
            return _proRepo.Update(existed);
        }
    }
}
