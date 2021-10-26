using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Create
{
    public class ClaimedVoucherCreateRequest
    {
        public ClaimedVoucherCreateRequest() { }
        public int UserId { get; set; }
        public int VoucherId { get; set; }
        public int Available { get; set; }
    }
}
