using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Create
{
    public partial class VoucherCreateRequest
    {
        public VoucherCreateRequest()
        {
        }
        public int PromotionId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int PercentDiscount { get; set; }
        public int MinRequiredAmount { get; set; }
        public int MaxDiscountAmount { get; set; }
        public int Quantity { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
