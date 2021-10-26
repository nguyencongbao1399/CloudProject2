using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Update

{
    public class BrandUpdateRequest
    {
        public BrandUpdateRequest()
        {
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }
}
