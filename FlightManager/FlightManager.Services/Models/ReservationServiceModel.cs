using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FlightManager.Domain.Enums;

namespace FlightManager.Services.Models
{
    public class ReservationServiceModel
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string EGN { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Nationality { get; set; }

        public TicketType TicketType { get; set; }

        public int TicketsCount { get; set; }

        public bool IsConfirmed { get; set; }

        public string FlightId { get; set; }
    }
}
