using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            AppliedPromotions = new HashSet<AppliedPromotion>();
            Vouchers = new HashSet<Voucher>();
        }

        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Description { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<AppliedPromotion> AppliedPromotions { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
