using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingVoucher_v02.Models;
using TrackingVoucher_v02.RequestModels.Create;

namespace TrackingVoucher_v02.Services
{
    public interface IAuthenticationService
    {
        public User Authentication(UserCreateRequest user);

        public User AuthenticationNoVerify(UserCreateRequest user);
    }
}
