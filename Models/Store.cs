using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.Models
{
    public partial class Store
    {
        public Store()
        {
            AppliedPromotions = new HashSet<AppliedPromotion>();
        }

        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<AppliedPromotion> AppliedPromotions { get; set; }
    }
}
