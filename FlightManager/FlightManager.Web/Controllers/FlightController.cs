using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Common;
using FlightManager.Services;
using FlightManager.Services.Models;
using FlightManager.Web.BindingModels.Flight;
using FlightManager.Web.ViewModels.Flight;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Controllers
{
    public class FlightController : Controller
    {
        private IFlightService flightService;

        public FlightController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create(Input input)
        {
            return View(input);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Create input)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Flight/Create");
            }

            var departureTime = new DateTime();

            if (!DateTime.TryParse(input.DepartureTime, out departureTime))
            {
                return Redirect("/Flight/Create");
            }

            var arrivalTime = new DateTime();

            if (!DateTime.TryParse(input.ArrivalTime, out arrivalTime))
            {
                return Redirect("/Flight/Create");
            }

            if (arrivalTime < departureTime)
            {
                return Redirect("/Flight/Create");
            }

            var flight = new CreateFlightServiceModel
            {
                From = input.From,
                To = input.To,
                ArrivalTime = arrivalTime,
                DepartureTime = departureTime,
                FreePassengersSeats = input.FreePassengersSeats,
                FreeBusinessSeats = input.FreeBusinessSeats,
                PlaneNumber = input.PlaneNumber,
                PlaneType = input.PlaneType,
                Image = input.Image,
                PilotName = input.PilotName
            };

            flightService.Create(flight);

            return Redirect("/Home/Index");
        }

        public IActionResult GetAll(int page)
        {
            if (page <= 0)
            {
                return Redirect("/Home/Index");
            }

            int flightsCount = flightService.GetCount();

            var lastPage = flightsCount / GlobalConstants.FlightsPerPage + 1;

            if (flightsCount % GlobalConstants.FlightsPerPage == 0)
            {
                lastPage--;
            }

            if (page > lastPage)
            {
                return Redirect("/Home/Index");
            }

            var flights = flightService.GetAll(page);

            var viewModel = new ListingPageViewModel
            {
                CurrentPage = page,
                TotalFlightsCount = flightsCount,
                LastPage = lastPage,
                Flights = new List<ListingViewModel>()
            };

            foreach (var flight in flights)
            {
                TimeSpan span = (flight.ArrivalTime - flight.DepartureTime);

                var travelTime = String.Format("{0} days/{1} hours/{2} minutes",
                    span.Days, span.Hours, span.Minutes, span.Seconds);

                viewModel.Flights.Add(new ListingViewModel()
                {
                    From = flight.From,
                    DepartureTime = flight.DepartureTime.ToString("MM/dd/yyyy hh:mm tt"),
                    To = flight.To,
                    Id = flight.Id,
                    TravelTime = travelTime,
                    FreePassengersSeats = flight.FreePassengersSeats,
                    FreeBusinessSeats = flight.FreeBusinessSeats
                });
            }

            return View(viewModel);
        }
    }
}
