using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManager.Services.Models
{
    public class FlightServiceModel
    {

        public string Id { get; set; }
        public string From { get; set; }

        public string To { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public string PlaneType { get; set; }

        public string PlaneNumber { get; set; }

        public string PilotName { get; set; }

        public int FreePassengersSeats { get; set; }

        public int FreeBusinessSeats { get; set; }

        public string Image { get; set; }
    }
}
