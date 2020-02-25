using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Domain;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public interface IReservationService
    {
        List<Reservation> GetAllByFlightId(string flightId);

        void Make(ReservationServiceModel input);

        bool HasWithId(string id);

        Reservation GetById(string id);

        void Confirm(string id);

        void Delete(string id);

        int GetCount();

        List<Reservation> GetAll(int page);
    }
}
