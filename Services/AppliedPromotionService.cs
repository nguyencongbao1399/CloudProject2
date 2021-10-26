using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.Repositories;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.Utils;

namespace TrackingVoucher_v02.Services
{
    public class AppliedPromotionService : IAppliedPromotionService
    {
        private readonly IAppliedPromotionRepository _repo;
        private readonly ValidateUtils util = new ValidateUtils();

        public AppliedPromotionService(IAppliedPromotionRepository repo)
        {
            _repo = repo;
        }
        public bool Create(AppliedPromotionCreateRequest entity)
        {
            int proId = entity.PromotionId;
            int storeId = entity.StoreId;
            AppliedPromotion existed = _repo.GetAll().FirstOrDefault(e => e.PromotionId == proId && e.StoreId == storeId);
            if (existed != null)
            {
                return false;
            }
            AppliedPromotion newEntity = new AppliedPromotion();
            newEntity.PromotionId = proId;
            newEntity.StoreId = storeId;
            return _repo.Create(newEntity);
        }

        public bool Delete(int id)
        {
            var entity = _repo.GetById(id);
            if (entity == null)
            {
                return false;
            }
            return _repo.Delete(entity);
        }

        public bool DeleteByPromotionId(int proId)
        {
            bool success = true;
            IEnumerable<AppliedPromotion> list = _repo.GetAll().Where(p => p.PromotionId == proId);
            foreach (AppliedPromotion item in list)
            {
                success = _repo.Delete(item);
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteByStoreId(int storeId)
        {
            bool success = true;
            IEnumerable<AppliedPromotion> list = _repo.GetAll().Where(p => p.StoreId == storeId);
            foreach (AppliedPromotion item in list)
            {
                success = _repo.Delete(item);
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<AppliedPromotion> GetAll(string query, int storeId, int promotionId, string ids, int startId)
        {
            //initialize a list
            IEnumerable<AppliedPromotion> list = _repo.GetAll();

            //case has query
            if (query.Length > 0)
            {
                //validate
                
                if (query.Contains("start-id="))
                {
                    if (!util.ValidIntParam(query, "start-id=", startId))
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
          
                if (query.Contains("store-id="))
                {
                    if (!util.ValidIntParam(query, "store-id=", storeId))
                    {
                        return null;
                    }
                }
          
                if (query.Contains("prmotion-id="))
                {
                    if (!util.ValidIntParam(query, "promotion-id=", promotionId))
                    {
                        return null;
                    }
                }

                //page only involve with limit and fields
                //not filter changing list item
                if (query.Contains("page=") && ( query.Contains("store-id=")
                                           || query.Contains("promotion-id=")
                                           || query.Contains("ids=")
                                           || query.Contains("start-id=")))
                {
                    return null;
                }

                //case param
                if (!query.Contains("&"))
                {

                    //name
                    if (query.Contains("store-id="))
                    {
                        return list.Where(e => e.StoreId == storeId);
                            
                    }

                    //address
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
                    if (query.Contains("store-id=") & query.Contains("promotion-id="))
                    {
                        return list.Where(e => e.StoreId == storeId && e.PromotionId == promotionId);
                            
                    }
                }
                return null;
            } // end if uri has query

            //case uri has no query
            return list;
        }

        public AppliedPromotion GetById(int id)
        {
            return _repo.GetById(id);
        }

        public int GetCount()
        {
            return _repo.GetAll().Count();
        }
    }
}
