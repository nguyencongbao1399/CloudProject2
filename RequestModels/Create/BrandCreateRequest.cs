using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Create

{
    public class BrandCreateRequest
    {
        public BrandCreateRequest()
        {
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
    }
}
