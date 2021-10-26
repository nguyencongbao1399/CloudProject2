using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Create
{
    public class StoreCreateRequest
    {
        public StoreCreateRequest()
        {
        }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
