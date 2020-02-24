using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Web.ViewModels.Flight
{
    public class ListingViewModel
    {
        public string Id { get; set; }
        public string From { get; set; }

        public string To { get; set; }

        public string DepartureTime { get; set; }

        public string TravelTime { get; set; }

        public int FreePassengersSeats { get; set; }

        public int FreeBusinessSeats { get; set; }
    }
}
