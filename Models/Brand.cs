using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Promotions = new HashSet<Promotion>();
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
