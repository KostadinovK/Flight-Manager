using System;
using System.Collections.Generic;
using System.Linq;
using FlightManager.Common;
using FlightManager.Data;
using FlightManager.Domain;
using FlightManager.Services.Models;
using Microsoft.EntityFrameworkCore;

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

        public int GetCount()
        {
            return context.Flights.Count();
        }

        public IEnumerable<Flight> GetAll(int page)
        {
            return context.Flights
                .OrderByDescending(f => f.DepartureTime)
                .Take(page * GlobalConstants.FlightsPerPage)
                .Skip((page - 1) * GlobalConstants.FlightsPerPage)
                .ToList();
        }
    }
}
