using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Update
{
    public class PromotionUpdateRequest
    {
        public PromotionUpdateRequest()
        {
        }
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Description { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
