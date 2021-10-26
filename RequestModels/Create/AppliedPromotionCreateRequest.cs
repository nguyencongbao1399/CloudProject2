using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Create
{
    public class AppliedPromotionCreateRequest
    {
        public AppliedPromotionCreateRequest()
        {
        }
        public int StoreId { get; set; }
        public int PromotionId { get; set; }
    }
}
