﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Web.ViewModels.User
{
    public class ListingViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}
