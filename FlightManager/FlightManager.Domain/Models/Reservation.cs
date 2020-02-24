using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FlightManager.Domain.Enums;

namespace FlightManager.Domain
{
    public class Reservation
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string SecondName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }


        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Enter Valid EGN!")]
        public string EGN { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nationality { get; set; }

        [Required]
        public TicketType TicketType { get; set; }

        public string FlightId { get; set; }

        public Flight Flight { get; set; }
    }
}
