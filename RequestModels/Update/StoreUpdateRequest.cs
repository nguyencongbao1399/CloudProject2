using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Update
{
    public class StoreUpdateRequest
    {
        public StoreUpdateRequest()
        {
        }
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
