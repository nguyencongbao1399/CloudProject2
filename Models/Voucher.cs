using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            ClaimedVouchers = new HashSet<ClaimedVoucher>();
        }

        public int Id { get; set; }
        public int PromotionId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int PercentDiscount { get; set; }
        public int? MinRequiredAmount { get; set; }
        public int? MaxDiscountAmount { get; set; }
        public int Quantity { get; set; }
        public int Available { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        public virtual Promotion Promotion { get; set; }
        public virtual ICollection<ClaimedVoucher> ClaimedVouchers { get; set; }
    }
}
