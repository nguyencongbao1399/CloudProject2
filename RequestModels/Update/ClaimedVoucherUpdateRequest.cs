using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Update
{
    public class ClaimedVoucherUpdateRequest
    {
        public ClaimedVoucherUpdateRequest() {
        }
        public int Id { get; set; }
        public int Available { get; set; }
    }
}
