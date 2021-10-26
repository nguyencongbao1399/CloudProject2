﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingVoucher_v02.RequestModels.Create
{
    public class UserCreateRequest
    {
        public UserCreateRequest()
        {
        }
        public string Gmail { get; set; }
        public string GmailToken { get; set; }
        public string Facebook { get; set; }
        public string FacebookToken { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
