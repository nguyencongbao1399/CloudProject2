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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IClaimedVoucherService _claimedSer;
        private readonly ValidateUtils util = new ValidateUtils();

        public UserService(IUserRepository userRepo, IClaimedVoucherService claimedSer)
        {
            _userRepo = userRepo;
            _claimedSer = claimedSer;
        }
        public bool Create(UserCreateRequest entity)
        {
            string gmail = entity.Gmail;
            string gmailToken = entity.GmailToken;
            string facebook = entity.Facebook;
            string facebookToken = entity.FacebookToken;
            string name = entity.Name;
            string img = entity.Img;
            string address = entity.Address;
            string phone = entity.Phone;
            DateTime dob = entity.BirthDate;

            if(gmail == null && facebook == null)
            {
                return false;
            }

            User newEntity = new User();
            newEntity.Name = name;
            newEntity.Address = address;
            newEntity.BirthDate = dob;
            newEntity.CreatedDate = DateTime.Now;
            newEntity.Img = img;
            newEntity.Phone = phone;
            newEntity.Gmail = gmail;
            newEntity.GmailToken = gmailToken;
            newEntity.Facebook = facebook;
            newEntity.FacebookToken = facebookToken;

            //facebook
            if(util.ValidRangeLengthInput(facebook, 1, 100) && util.ValidRangeLengthInput(facebookToken, 1, 500))
            {
                if (!_userRepo.ExistedFacebook(facebook))
                {
                    User existed = _userRepo.GetAll().FirstOrDefault(e => e.Facebook.Equals(facebook) || e.FacebookToken.Equals(facebookToken));
                    if (existed==null)
                    {
                        return _userRepo.Create(newEntity);
                    }                 
                }                
            }

            //gmail
            if(util.ValidRangeLengthInput(gmail, 1, 100) && util.ValidRangeLengthInput(gmailToken, 1, 500)){
                if (!_userRepo.ExistedGmail(gmail))
                {
                    User existed = _userRepo.GetAll().FirstOrDefault(e => e.Gmail.Equals(gmail) || e.GmailToken.Equals(gmailToken));
                    if (existed == null)
                    {
                        return _userRepo.Create(newEntity);
                    }
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            _claimedSer.DeleteByUserId(id);
            var entity = _userRepo.GetById(id);
            if (entity == null)
            {
                return false;
            }
            return _userRepo.Delete(entity);
        }

        public IEnumerable<User> GetAll(string query, string name, string address, string ids, int startId)
        {
            //initialize a list
            IEnumerable<User> list = _userRepo.GetAll();

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

                //case param
                if (!query.Contains("&"))
                {

                    //name
                    if (query.Contains("name="))
                    {
                        return list.Where(brand => brand.Name.Equals(name));

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

        public User GetByFacebook(string facebook)
        {
            return _userRepo.GetByFacebook(facebook);
        }

        public User GetByGmail(string gmail)
        {
            return _userRepo.GetByGmail(gmail);
        }

        public User GetById(int id)
        {
            return _userRepo.GetById(id);
        }

        public int GetCount()
        {
            return _userRepo.GetAll().Count();
        }

        public bool Update(UserUpdateRequest entity)
        {
            User existed = _userRepo.GetById(entity.Id);
            if (existed!=null)
            {
                return false;
            }
            string name = entity.Name;
            string img = entity.Img;
            string address = entity.Address;
            string phone = entity.Phone;
            DateTime dob = entity.BirthDate;

            if (address != null)
            {
                if (!util.ValidRangeLengthInput(address, 1, 250))
                {
                    return false;
                }
                existed.Address = address.Trim();
            }
            if (name != null)
            {
                if (!util.ValidRangeLengthInput(name, 1, 100))
                {
                    return false;
                }
                existed.Name = name.Trim();
            }
            if (img != null)
            {
                if (!util.ValidRangeLengthInput(img, 1, 100))
                {
                    return false;
                }
                existed.Img = img.Trim();
            }
            if (phone != null)
            {
                if (!util.ValidFixedLengthInput(phone,10))
                {
                    return false;
                }
                existed.Phone = phone.Trim();
            }
            if (dob != null)
            {
                if (dob.AddDays(365*10).CompareTo(DateTime.Now) >= 0)
                {
                    return false;
                }
                existed.BirthDate = dob;
            }
            existed.UpdatedDate = DateTime.Now;
            return _userRepo.Update(existed);
        }
    }
}
