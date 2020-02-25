using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlightManager.Data;
using FlightManager.Domain;

namespace FlightManager.Services
{
    public class ReservationService : IReservationService
    {
        private FlightManagerDbContext context;

        public ReservationService(FlightManagerDbContext context)
        {
            this.context = context;
        }

        public List<Reservation> GetAllByFlightId(string flightId)
        {
            return context.Reservations.Where(r => r.FlightId == flightId).ToList();
        }
    }
}
