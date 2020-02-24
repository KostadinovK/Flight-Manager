using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Services;
using FlightManager.Services.Models;
using FlightManager.Web.BindingModels.Flight;
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

        public IActionResult Create(Input input)
        {
            return View(input);
        }

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
    }
}
