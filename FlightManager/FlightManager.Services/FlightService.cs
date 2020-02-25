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

        public void Create(FlightServiceModel input)
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

        public bool HasWithId(string id)
        {
            return context.Flights.Any(f => f.Id == id);
        }

        public Flight GetById(string id)
        {
            if (!HasWithId(id))
            {
                throw new ArgumentException("Invalid flight id!");
            }

            var flight = context.Flights.SingleOrDefault(f => f.Id == id);

            return flight;
        }

        public void DeleteById(string id)
        {
            if (!HasWithId(id))
            {
                throw new ArgumentException("Invalid flight id!");
            }

            var flight = context.Flights.SingleOrDefault(f => f.Id == id);

            context.Flights.Remove(flight);
            context.SaveChanges();
        }

        public void Edit(FlightServiceModel flight)
        {
            if (!HasWithId(flight.Id))
            {
                throw new ArgumentException("Invalid flight id!");
            }

            var flightFromDb = context.Flights.SingleOrDefault(f => f.Id == flight.Id);

            flightFromDb.From = flight.From;
            flightFromDb.To = flight.To;
            flightFromDb.ArrivalTime = flight.ArrivalTime;
            flightFromDb.DepartureTime = flight.DepartureTime;
            flightFromDb.FreePassengersSeats = flight.FreePassengersSeats;
            flightFromDb.FreeBusinessSeats = flight.FreeBusinessSeats;
            flightFromDb.PlaneNumber = flight.PlaneNumber;
            flightFromDb.PlaneType = flight.PlaneType;
            flightFromDb.Image = flight.Image;
            flightFromDb.PilotName = flight.PilotName;

            context.Flights.Update(flightFromDb);
            context.SaveChanges();
        }
    }
}
