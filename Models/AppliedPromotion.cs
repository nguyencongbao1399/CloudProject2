using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.Models
{
    public partial class AppliedPromotion
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int PromotionId { get; set; }

        public virtual Promotion Promotion { get; set; }
        public virtual Store Store { get; set; }
    }
}
