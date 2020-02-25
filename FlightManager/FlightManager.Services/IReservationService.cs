using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Domain;

namespace FlightManager.Services
{
    public interface IReservationService
    {
        List<Reservation> GetAllByFlightId(string flightId);
    }
}
