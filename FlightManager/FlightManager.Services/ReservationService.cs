using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using FlightManager.Common;
using FlightManager.Data;
using FlightManager.Domain;
using FlightManager.Domain.Enums;
using FlightManager.Services.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FlightManager.Services
{
    public class ReservationService : IReservationService
    {
        private FlightManagerDbContext context;
        private IEmailSender emailSender;

        public ReservationService(FlightManagerDbContext context, IEmailSender emailSender)
        {
            this.context = context;
            this.emailSender = emailSender;
        }

        public List<Reservation> GetAllByFlightId(string flightId)
        {
            return context.Reservations.Where(r => r.FlightId == flightId).ToList();
        }

        public void Make(ReservationServiceModel input)
        {
            if (input.TicketsCount <= 0)
            {
                return;
            }

            var flight = context.Flights.SingleOrDefault(f => f.Id == input.FlightId);

            if (input.TicketType == TicketType.Business && input.TicketsCount > flight.FreeBusinessSeats)
            {
                return;
            }

            if (input.TicketType == TicketType.Normal && input.TicketsCount > flight.FreePassengersSeats)
            {
                return;
            }

            var reservation = new Reservation()
            {
                FirstName = input.FirstName,
                SecondName = input.SecondName,
                LastName = input.LastName,
                EGN = input.EGN,
                Email = input.Email,
                FlightId = input.FlightId,
                IsConfirmed = false,
                Nationality = input.Nationality,
                TicketsCount = input.TicketsCount,
                TicketType = input.TicketType,
                PhoneNumber = input.PhoneNumber
            };

            context.Reservations.Add(reservation);
            context.SaveChanges();

            var message = $@"Dear {reservation.FirstName} {reservation.LastName}, do you wish to confirm your reservation for {reservation.TicketsCount} {reservation.TicketType} Tickets from {flight.From} to {flight.To}?
              <br/>
               <a href='{GlobalConstants.AppAddress}Reservation/Confirm?id={reservation.Id}'>Click here</a> to confirm it.
                <br/>
                <a href='{GlobalConstants.AppAddress}Reservation/Delete?id={reservation.Id}'>Click here</a> to delete it.
            ";

            emailSender.SendEmailAsync(reservation.Email, "Confirm Your Reservation", message).GetAwaiter().GetResult();
        }

        public bool HasWithId(string id)
        {
            return context.Reservations.Any(r => r.Id == id);
        }

        public Reservation GetById(string id)
        {
            if (!HasWithId(id))
            {
                throw new ArgumentException("Invalid reservation id");
            }

            return context.Reservations.SingleOrDefault(r => r.Id == id);
        }

        public void Confirm(string id)
        {
            if (!HasWithId(id))
            {
                throw new ArgumentException("Invalid reservation id!");
            }

            var reservation = GetById(id);

            reservation.IsConfirmed = true;

            var flight = context.Flights.SingleOrDefault(f => f.Id == reservation.FlightId);

            if (reservation.TicketType == TicketType.Business)
            {
                flight.FreeBusinessSeats -= reservation.TicketsCount;
            }

            if (reservation.TicketType == TicketType.Normal)
            {
                flight.FreePassengersSeats -= reservation.TicketsCount;
            }

            context.Flights.Update(flight);
            context.Reservations.Update(reservation);

            context.SaveChanges();
        }

        public void Delete(string id)
        {
            if (!HasWithId(id))
            {
                throw new ArgumentException("Invalid reservation id");
            }

            var reservation = GetById(id);

            if (reservation.IsConfirmed)
            {
                return;
            }

            context.Reservations.Remove(reservation);
            context.SaveChanges();
        }

        public int GetCount()
        {
            return context.Reservations.Count();
        }

        public List<Reservation> GetAll(int page)
        {
            return context.Reservations
                .OrderByDescending(r => r.Email)
                .Take(page * GlobalConstants.ReservationsPerPage)
                .Skip((page - 1) * GlobalConstants.ReservationsPerPage)
                .ToList();
        }
    }
}
