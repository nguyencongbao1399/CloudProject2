using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.Models
{
    public partial class ClaimedVoucher
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VoucherId { get; set; }
        public int Available { get; set; }
        public DateTime ClaimedDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime? LastUsedDate { get; set; }

        public virtual User User { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}
