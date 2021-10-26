using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Create
{
    public class PromotionCreateRequest
    {
        public PromotionCreateRequest()
        {
        }
        public int BrandId { get; set; }
        public string Description { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
