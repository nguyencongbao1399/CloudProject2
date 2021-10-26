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
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _vouRepo;
        private readonly IClaimedVoucherService _claimedSer;
        private readonly ValidateUtils util = new ValidateUtils();

        public VoucherService(IVoucherRepository vouRepo, IClaimedVoucherService claimedSer)
        {
            _vouRepo = vouRepo;
            _claimedSer = claimedSer;
        }
        public bool Create(VoucherCreateRequest entity)
        {
            string description = entity.Description;
            int quantity = entity.Quantity;
            int available = quantity;
            string code = entity.Code;
            DateTime beginDate = entity.BeginDate;
            DateTime expiredDate = entity.ExpiredDate;
            int maxDiscount = entity.MaxDiscountAmount;
            int minRequiredAmount = entity.MinRequiredAmount;
            int percentDiscount = entity.PercentDiscount;
            int proId = entity.PromotionId;

            if (!util.ValidRangeLengthInput(description, 1, 250)
                || !util.ValidRangeLengthInput(code, 1, 20)
                || beginDate.CompareTo(expiredDate) == 1
                || quantity < 0
                || percentDiscount <= 0 || maxDiscount <= 0 || minRequiredAmount <= 0
                )
            {
                return false;
            }

            Voucher existed = _vouRepo.GetAll().FirstOrDefault(e => e.Code.Trim().ToLower().Equals(code.Trim().ToLower()));
            if (existed != null)
            {
                return false;
            }
            Voucher newEntity = new Voucher();
            newEntity.Description = description;
            newEntity.Quantity = quantity;
            newEntity.Available = available;
            newEntity.Code = code;
            newEntity.BeginDate = beginDate;
            newEntity.ExpiredDate = expiredDate;
            newEntity.MinRequiredAmount = minRequiredAmount;
            newEntity.MaxDiscountAmount = maxDiscount;
            newEntity.PercentDiscount = percentDiscount;
            newEntity.PromotionId = proId;
            return _vouRepo.Create(newEntity);
        }

        public bool Delete(int id)
        {
            var entity = _vouRepo.GetById(id);
            if (entity == null)
            {
                return false;
            }
            return _vouRepo.Delete(entity);
        }

        public bool DeleteByPromotionId(int proId)
        {
            bool success = true;
            IEnumerable<Voucher> list = _vouRepo.GetAll().Where(p => p.PromotionId == proId);
            foreach (Voucher item in list)
            {
                _claimedSer.DeleteByVoucherId(item.Id);
                success = _vouRepo.Delete(item);
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<Voucher> GetAll(string query, int promotionId, string ids, 
            int startId, DateTime beginDate, DateTime expiredDate)
        {
            //initialize a list
            IEnumerable<Voucher> list = _vouRepo.GetAll();

            //case has query
            if (query.Length > 0)
            {
                //validate

                if (query.Contains("promotion-id="))
                {
                    if (!util.ValidIntParam(query, "promotion-id=", promotionId))
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
                if (query.Contains("promotion-id=") && query.Contains("ids="))
                {
                    return null;
                }

                //page only involve with limit and fields
                //not filter changing list item
                if (query.Contains("page=") && (query.Contains("promotion-id=")
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
                    if (query.Contains("promotion-id="))
                    {
                        return list.Where(e => e.PromotionId == promotionId);
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

        public Voucher GetById(int id)
        {
            return _vouRepo.GetById(id);
        }

        public int GetCount()
        {
            return _vouRepo.GetAll().Count();
        }

        public bool Update(VoucherUpdateRequest entity)
        {
            Voucher existed = _vouRepo.GetById(entity.Id);
            if (existed==null)
            {
                return false;
            }
            string description = entity.Description;
            int quantity = entity.Quantity;
            string code = entity.Code;
            DateTime beginDate = entity.BeginDate;
            DateTime expiredDate = entity.ExpiredDate;
            int maxDiscount = entity.MaxDiscountAmount;
            int minRequiredAmount = entity.MinRequiredAmount;
            int percentDiscount = entity.PercentDiscount;
            int proId = entity.PromotionId;
            if (description != null)
            {
                if (!util.ValidRangeLengthInput(description, 1, 250))
                {
                    return false;
                }
                existed.Description = description.Trim();
            }
            if (quantity > 0)
            {
                int used = existed.Quantity - existed.Available;
                if(quantity < used)
                {
                    return false;
                }
                int marginQuantity = quantity - existed.Quantity;
                existed.Quantity = quantity;
                existed.Available += marginQuantity;
            }
            if (code != null)
            {
                if (!util.ValidRangeLengthInput(code, 1, 20))
                {
                    return false;
                }
                Voucher existedCode = _vouRepo.GetAll().FirstOrDefault(e => e.Code.Trim().ToLower().Equals(code.Trim().ToLower()));
                if (existedCode != null)
                {
                    return false;
                }
                existed.Code = code;
            }
            if (maxDiscount > 0)
            {
                existed.MaxDiscountAmount = maxDiscount;
            }
            if (minRequiredAmount > 0)
            {
                existed.MinRequiredAmount = minRequiredAmount;
            }
            if (percentDiscount > 0)
            {
                existed.PercentDiscount = percentDiscount;
            }
            if (proId > 0)
            {
                existed.PromotionId = proId;
            }
            if (beginDate != null && expiredDate != null)
            {
                if (beginDate.CompareTo(expiredDate) >= 0)
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
            return _vouRepo.Update(existed);
        }
    }
}
