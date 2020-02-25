using System;
using System.Collections.Generic;
using FlightManager.Common;
using FlightManager.Services;
using FlightManager.Services.EmailSender;
using FlightManager.Services.Models;
using FlightManager.Web.BindingModels.Reservation;
using FlightManager.Web.ViewModels.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Controllers
{
    public class ReservationController : Controller
    {
        private IFlightService flightService;
        private IReservationService reservationService;
       

        public ReservationController(IFlightService flightService, IReservationService reservationService, IEmailSender emailSender)
        {
            this.flightService = flightService;
            this.reservationService = reservationService;
        }

        public IActionResult Make(string id)
        {
            if (!flightService.HasWithId(id))
            {
                return Redirect("/Home/Index");
            }

            var reservation = new CreateBindingModel { };
            reservation.FlightId = id;

            return View(reservation);
        }

        [HttpPost]
        public IActionResult Make(CreateBindingModel input)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Flight/Create");
            }

            var flightId = input.FlightId;

            var reservation = new ReservationServiceModel
            {
                FirstName = input.FirstName,
                SecondName = input.SecondName,
                LastName = input.LastName,
                EGN = input.EGN,
                PhoneNumber = input.PhoneNumber,
                Nationality = input.Nationality,
                TicketType = input.TicketType,
                TicketsCount = input.TicketsCount,
                IsConfirmed = false,
                FlightId = input.FlightId,
                Email = input.Email
            };

            reservationService.Make(reservation);

            return Redirect("/Home/Index");
        }

        public IActionResult Confirm(string id)
        {
            if (!reservationService.HasWithId(id))
            {
                return Redirect("/Home/Index");
            }

            reservationService.Confirm(id);

            return Redirect("/Home/Index");
        }

        public IActionResult Delete(string id)
        {
            if (!reservationService.HasWithId(id))
            {
                return Redirect("/Home/Index");
            }

            reservationService.Delete(id);

            return Redirect("/Home/Index");
        }

        [Authorize]
        public IActionResult All(int page)
        {
            if (page <= 0)
            {
                return Redirect("/Home/Index");
            }

            int reservationsCount = reservationService.GetCount();

            var lastPage = reservationsCount / GlobalConstants.ReservationsPerPage + 1;

            if (reservationsCount % GlobalConstants.ReservationsPerPage == 0 && lastPage > 1)
            {
                lastPage--;
            }

            if (page > lastPage)
            {
                return Redirect("/Home/Index");
            }

            var reservations = reservationService.GetAll(page);

            var viewModel = new ListingPageViewModel
            {
                CurrentPage = page,
                TotalReservationsCount = reservationsCount,
                LastPage = lastPage,
                Reservations = new List<ReservationViewModel>()
            };

            foreach (var reservation in reservations)
            {
                viewModel.Reservations.Add(new ReservationViewModel()
                {
                    FirstName = reservation.FirstName,
                    SecondName = reservation.SecondName,
                    LastName = reservation.LastName,
                    EGN = reservation.EGN,
                    Email = reservation.Email,
                    Id = reservation.Id,
                    IsConfirmed = reservation.IsConfirmed,
                    Nationality = reservation.Nationality,
                    PhoneNumber = reservation.PhoneNumber,
                    TicketType = reservation.TicketType.ToString(),
                    TicketsCount = reservation.TicketsCount
                });
            }

            return View(viewModel);
        }
    }
}
