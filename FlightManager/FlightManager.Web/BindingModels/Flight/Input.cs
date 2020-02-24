using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Web.BindingModels.Flight
{
    public class Input
    {
        [Required]
        [MaxLength(30)]
        public string From { get; set; }

        [Required]
        [MaxLength(30)]
        public string To { get; set; }

        [Required]
        public string DepartureTime { get; set; }

        [Required]
        public string ArrivalTime { get; set; }

        [Required]
        public string PlaneType { get; set; }

        [Required]
        public string PlaneNumber { get; set; }

        [Required]
        [MaxLength(30)]
        public string PilotName { get; set; }

        [Required]
        [Range(0, 100000)]
        public int FreePassengersSeats { get; set; }

        [Required]
        [Range(0, 100000)]
        public int FreeBusinessSeats { get; set; }

        public string Image { get; set; }
    }
}
