using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;

namespace TrackingVoucher_v02.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userSer;

        public AuthenticationService(IUserService userSer)
        {
            _userSer = userSer;
        }
        public User Authentication(UserCreateRequest user)
        {
            //FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(encodeToken);
            //var uid = decodedToken.Uid;
            return null;
        }

        public User AuthenticationNoVerify(UserCreateRequest user)
        {
            string gmail = user.Gmail;
            string facebook = user.Facebook;
            bool success = _userSer.Create(user);
            if (success)
            {
                if (gmail!=null)
                {
                    return _userSer.GetByGmail(gmail);
                }
                if (facebook!=null)
                {
                    return _userSer.GetByFacebook(facebook);
                }
            }
            return null;
        }
    }
}
