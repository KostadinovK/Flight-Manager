using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Web.ViewModels.Reservation
{
    public class ListingPageViewModel
    {
        public List<ReservationViewModel> Reservations { get; set; }

        public int TotalReservationsCount { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }
    }
}
