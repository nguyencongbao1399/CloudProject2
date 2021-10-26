using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.Repositories;
using TrackingVoucher_v02.RequestModels.Update;
using TrackingVoucher_v02.RequestModels.Create;
using TrackingVoucher_v02.Utils;

namespace TrackingVoucher_v02.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repo;
        private readonly IPromotionService _proSer;
        private readonly IStoreService _storeSer;
        private readonly ValidateUtils util = new ValidateUtils();

        public BrandService(IBrandRepository repo, IPromotionService proSer, IStoreService storeSer)
        {
            _repo = repo;
            _proSer = proSer;
            _storeSer = storeSer;
        }
        public bool Create(BrandCreateRequest entity)
        {
            string name = entity.Name;
            string address = entity.Address;
            string phone = entity.Phone;
            string website = entity.Website;

            if (!util.ValidRangeLengthInput(name, 1, 100)
                || !util.ValidRangeLengthInput(address, 1, 250)
                || !util.ValidRangeLengthInput(website, 1, 100)
                || !util.ValidFixedLengthInput(phone, 10))
            {
                return false;
            }

            Brand exsited = _repo.GetAll().FirstOrDefault(e => e.Name.ToLower().Equals(name.Trim().ToLower()));
            if (exsited != null)
            {
                return false;
            }

            Brand newEntity = new Brand();
            newEntity.Name = name.Trim();
            newEntity.Address = address.Trim();
            newEntity.Phone = phone.Trim();
            newEntity.Website = website.Trim();

            return _repo.Create(newEntity);
        }

        public bool Delete(int id)
        {
            _proSer.DeleteByBrandId(id);
            _storeSer.DeleteByBrandId(id);
            var entity = _repo.GetById(id);
            if (entity == null)
            {
                return false;
            }
            return _repo.Delete(entity);
        }

        public IEnumerable<Brand> GetAll(string query, string name, string address, string ids, int startId)
        {
            //initialize a list
            IEnumerable<Brand> list = _repo.GetAll();

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

                //page only involve with limit and fields
                //not filter changing list item
                if (query.Contains("page=") && ( query.Contains("name=")
                                           || query.Contains("address=")
                                           || query.Contains("ids=")
                                           || query.Contains("start-id=")))
                {
                    return null;
                }

                //case param
                if (!query.Contains("&"))
                {

                    //name
                    if (query.Contains("name="))
                    {
                        return list.Where(brand => brand.Name.ToLower().Equals(name.Trim().ToLower()));
                            

                    }

                    //address
                    if (query.Contains("address="))
                    {
                        return list.Where(brand => brand.Address.Equals(address));
                            
                    }

                    //ids
                    if (query.Contains("ids="))
                    {
                        return list.Where(brand => ids.Contains(brand.Id.ToString()));
                            
                    }

                    //startId
                    if (query.Contains("start-id="))
                    {
                        return list.Where(brand => brand.Id >= startId);
                            
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
                    if (query.Contains("name=") & query.Contains("address="))
                    {
                        return list.Where(brand => brand.Name.Equals(name) && brand.Address.Equals(address));
                            
                    }

                }
                return null;
            } // end if uri has query

            //case uri has no query
            return list;
        }

        public Brand GetById(int id)
        {
            return _repo.GetById(id);
        }

        public int GetCount()
        {
            return _repo.GetAll().Count();
        }

        public bool Update(BrandUpdateRequest entity)
        {
            string phone = entity.Phone;
            string website = entity.Website;
            string address = entity.Address;

            Brand existed = _repo.GetById(entity.Id);
            if (existed == null)
            {
                return false;
            }
            
            if(phone != null)
            {
                if(!util.ValidFixedLengthInput(phone, 10))
                {
                    return false;
                }
                existed.Phone = phone.Trim();
            }
            
            if(website!=null)
            {
                if(!util.ValidRangeLengthInput(website, 1, 100))
                {
                    return false;
                }
                existed.Website = website.Trim();
            }  

            if (address != null)
            {
                if(!util.ValidRangeLengthInput(address, 1, 250))
                {
                    return false;
                }
                existed.Address = address.Trim();
            }

            return _repo.Update(existed);
        }
    }
}
