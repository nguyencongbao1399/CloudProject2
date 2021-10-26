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
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _repo;
        private readonly ValidateUtils util = new ValidateUtils();

        public StoreService(IStoreRepository repo)
        {
            _repo = repo;
        }
        public bool Create(StoreCreateRequest entity)
        {
            int brandId = entity.BrandId;
            string name = entity.Name;
            string address = entity.Address;
            string phone = entity.Phone;

            if (!util.ValidRangeLengthInput(name, 1, 100)
                || !util.ValidRangeLengthInput(address, 1, 250)
                || !util.ValidFixedLengthInput(phone, 10))
            {
                return false;
            }

            Store existed = _repo.GetAll().FirstOrDefault(e => e.Name.ToLower().Equals(name.ToLower()));

            if (existed!=null)
            {
                return false;
            }

            Store newEntity = new Store();
            newEntity.Name = name.Trim();
            newEntity.BrandId = brandId;
            newEntity.Phone = phone.Trim();
            newEntity.Address = address.Trim();

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

        public bool DeleteByBrandId(int brandId)
        {
            bool success = true;
            IEnumerable<Store> list = _repo.GetAll().Where(p => p.BrandId == brandId);
            foreach (Store item in list)
            {
                success = _repo.Delete(item);
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<Store> GetAll(string query, int brandId, string name, string address,
            string ids, int startId)
        {
            //initialize a list
            IEnumerable<Store> list = _repo.GetAll();

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
         
                if (query.Contains("name="))
                {
                    if (!util.ValidStringParam(query, "name=", name))
                    {
                        return null;
                    }
                }
        
                if (query.Contains("address="))
                {
                    if (!util.ValidStringParam(query, "address=", address))
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
                                           || query.Contains("name=")
                                           || query.Contains("address=")
                                           || query.Contains("ids=")
                                           || query.Contains("start-id=")))
                {
                    return null;
                }

                //case param
                if (!query.Contains("&"))
                {

                    //id
                    if (query.Contains("brand-id="))
                    {
                        return list.Where(store => store.BrandId == brandId);
                    }

                    //name
                    if (query.Contains("name="))
                    {
                        return list.Where(store => store.Name.Equals(name));
                            

                    }

                    //address
                    if (query.Contains("address="))
                    {
                        return list.Where(store => store.Address.Equals(address));
                            
                    }

                    //ids
                    if (query.Contains("ids="))
                    {
                        return list.Where(store => ids.Contains(store.Id.ToString()));
                            
                    }

                    //startId
                    if (query.Contains("start-id="))
                    {
                        return list.Where(store => store.Id >= startId);
                            
                    }
                }

                //case params
                if (query.Contains("&"))
                {
                    //case params: name, address
                    if (query.Contains("name=") & query.Contains("address="))
                    {
                        return list.Where(store => store.Name.Equals(name) && store.Address.Equals(address));
                            
                    }

                    //case params: id, address
                    if (query.Contains("brand-id=") && query.Contains("address="))
                    {
                        return list.Where(store => store.BrandId == brandId && store.Address.Equals(address));
                            
                    }

                    //case params: id, name
                    if (query.Contains("brand-id=") && query.Contains("name="))
                    {
                        return list.Where(store => store.BrandId == brandId && store.Name.Equals(name));
                            
                    }

                    //case params: id, name, address
                    if (query.Contains("brand-id=") && query.Contains("name=") && query.Contains("address="))
                    {
                        return list.Where(store => store.BrandId == brandId && store.Name.Equals(name)
                        && store.Address.Equals(address));
                            
                    }

                }
                return null;
            } // end if uri has query

            //case uri has no query
            return list;
        }

        public Store GetById(int id)
        {
            return _repo.GetById(id);
        }

        public int GetCount()
        {
            return _repo.GetAll().Count();
        }

        public bool Update(StoreUpdateRequest entity)
        {
            Store existed = _repo.GetById(entity.Id);
            if (existed==null)
            {
                return false;
            }
            int brandId = entity.BrandId;
            string name = entity.Name;
            string address = entity.Address;
            string phone = entity.Phone;
            if (brandId > 0)
            {
                existed.BrandId = brandId;
            }
            if (name != null)
            {
                if(!util.ValidRangeLengthInput(name, 1, 100))
                {
                    return false;
                }
                Store existedName = _repo.GetAll().FirstOrDefault(e => e.Name.ToLower().Equals(name.ToLower()));

                if (existedName != null)
                {
                    return false;
                }
                existed.Name = name.Trim();
            }
            if (phone != null)
            {
                if (!util.ValidFixedLengthInput(phone, 10))
                {
                    return false;
                }
                existed.Phone = phone.Trim();
            }
            if (address != null)
            {
                if (!util.ValidRangeLengthInput(address, 1, 250))
                {
                    return false;
                }
                existed.Address = address.Trim();
            }
            return _repo.Update(existed);
        }
    }
}
