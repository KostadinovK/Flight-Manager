using System;
using FlightManager.Data;
using FlightManager.Domain;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightManagerDbContext context;

        public FlightService(FlightManagerDbContext context)
        {
            this.context = context;
        }

        public void Create(CreateFlightServiceModel input)
        {
            var flight = new Flight
            {
                From = input.From,
                To = input.To,
                ArrivalTime = input.ArrivalTime,
                DepartureTime = input.DepartureTime,
                FreePassengersSeats = input.FreePassengersSeats,
                FreeBusinessSeats = input.FreeBusinessSeats,
                PlaneNumber = input.PlaneNumber,
                PlaneType = input.PlaneType,
                Image = input.Image,
                PilotName = input.PilotName
            };

            context.Flights.Add(flight);
            context.SaveChanges();
        }
    }
}
