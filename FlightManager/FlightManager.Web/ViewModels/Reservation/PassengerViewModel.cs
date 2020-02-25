﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Web.ViewModels.Reservation
{
    public class PassengerViewModel
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Nationality { get; set; }

        public string TicketType { get; set; }
    }
}
