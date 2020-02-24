using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Web.ViewModels.Flight
{
    public class ListingPageViewModel
    {
        public List<ListingViewModel> Flights { get; set; }

        public int TotalFlightsCount { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }
    }
}
