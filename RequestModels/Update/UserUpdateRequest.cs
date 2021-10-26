using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Update
{
    public class UserUpdateRequest
    {
        public UserUpdateRequest()
        {
        }
        public int Id { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
